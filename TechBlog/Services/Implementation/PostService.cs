using AutoMapper;
using Data_Access;
using Data_Access.Implementations;
using Data_Access.Interfaces;
using Domain_Models;
using DTOs.FilterDto;
using DTOs.Image;
using DTOs.Post;
using Mappers.MapperConfig;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Services.Interfaces;
using System;
using System.Collections.Generic;

namespace Services.Implementation
{
    public class PostService : IPostService
    {
        private readonly IImageService _imageService;
        private readonly IPostRepository _repository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly IUserService _userService;
        private readonly TechBlogDbContext _context;
        private readonly DbSet<Post> _table;

        public PostService(IPostRepository repo, IMapper mapper, IEmailService emailService, IUserService userService, TechBlogDbContext table, IImageService imageService)
        {
            _imageService = imageService;
            _repository = repo;
            _mapper = mapper;
            _emailService = emailService;
            _userService = userService;
            _context = table;
            _table = _context.Set<Post>();
        }
        public bool Add(PostCreateDto entity)
        {
            var newPost = _mapper.Map<Post>(entity);

            if (entity.ImageId.HasValue)
            {

                var imageFound = _imageService.GetById(entity.ImageId);

                if (imageFound != null)
                {
                    newPost.ImageBase64 = imageFound.Data;
                }
            }

            if (entity.ImageFile != null)
            {
                //using (var memoryStream = new MemoryStream())
                //{
                //    entity.ImageFile.CopyTo(memoryStream);
                //    var imageBytes = memoryStream.ToArray();
                //    string base64String = Convert.ToBase64String(imageBytes);
                //    newPost.ImageBase64 = base64String;
                //}
                newPost.ImageBase64 = entity.ImageFile;
            }

            newPost.PostingTime = DateTime.UtcNow;

            if (_repository.Add(newPost))
            {
                var author = _userService.GetUserById(newPost.UserId);

                if (author == null)
                {
                    return false;
                }
                _emailService.SendEmailToSubscribers(entity);

                return true;
            }
            return false;
        }

        public bool Any(int id) => _repository.Any(id);
        public bool DeleteById(int id)
        {
            if (!_repository.Any(id)) return false;

            return _repository.DeleteById(id);
        }


        //  I wonder if this 1 liner will cause any issues 
        public ICollection<PostDto> GetAll() => _mapper.Map<ICollection<PostDto>>(_repository.GetAllPostsIncludingUsers().ToList());
        //  It wasn't the method's fault :]

        public async Task<PaginatedListDto> GetPaginatedPosts(int pageIndex, PostFilter filters)
        {
            var query = _table.AsQueryable();

            if (!string.IsNullOrEmpty(filters.SortBy))
            {
                switch (filters.SortBy)
                {
                    case "old":
                        query = query.OrderBy(b => b.PostingTime);
                        break;
                    case "new":
                        query = query.OrderByDescending(b => b.PostingTime);
                        break;
                    case "popular":
                        query = query
                            .OrderByDescending(b => b.Stars.Any() ? b.Stars.Average(s => s.Rating) : 0);
                        break;
                }
            }

            if (filters.Tags != null && filters.Tags.Any())
            {
                foreach (var tag in filters.Tags)
                {
                    var currentTag = tag;
                    query = query.Where(p => p.Tags.Contains(currentTag));
                }
            }

            if (filters.Year.HasValue && filters.Year != 0)
            {
                query = query.Where(p => p.PostingTime.Year == filters.Year.Value);
            }

            if (filters.Month.HasValue && filters.Month != 0)
            {
                query = query.Where(p => p.PostingTime.Month == filters.Month.Value);
            }

            var result = await _repository.GetPaginatedPosts(pageIndex, query);
            return _mapper.Map<PaginatedListDto>(result);
        }

        public PostDetailsDto? GetById(int id)
        {
            try
            {
                var result = _repository.GetDetailedPost(id);
                var mappedResult = _mapper.Map<PostDetailsDto>(result);
                return mappedResult;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Update(PostUpdateDto entity)
        {
            var found = _repository.GetById(entity.Id);
            if (found != null)
            {
                found.Title = entity.Title;
                found.Text = entity.Text;
                found.Description = entity.Description;
                //found.UserId = entity.UserId; -   No, we can't change the author of the post!
                //found.Image = entity.Image; -     And I'm not sure if we want to be able to change the image(too much work imo)
                //found.Tags = entity.Tags.GetPostTags();

                return _repository.Update(found);
            }
            return false;
        }

        public List<PostDto> GetUserPosts(int userId)
        {
            var posts = _repository.GetUserPosts(userId);
            return _mapper.Map<List<PostDto>>(posts);
        }
    }
}
