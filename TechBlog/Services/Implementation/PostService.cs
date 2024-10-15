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
using Services.Interfaces;
using System.Collections.Generic;

namespace Services.Implementation
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _repository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly IUserService _userService;
        private readonly TechBlogDbContext _context;
        private readonly DbSet<Post> _table;

        public PostService(IPostRepository repo, IMapper mapper, IEmailService emailService, IUserService userService, TechBlogDbContext table)
        {
            _repository = repo;
            _mapper = mapper;
            _emailService = emailService;
            _userService = userService;
            _context = table;
            _table = _context.Set<Post>();
        }
        public bool Add(PostCreateDto entity)
        {
            using (var memoryStream = new MemoryStream())
            {
                entity.ImageFile.CopyTo(memoryStream);
                var imageBytes = memoryStream.ToArray();
                string base64String = Convert.ToBase64String(imageBytes);

                var post = _mapper.Map<Post>(entity);
                post.ImageBase64 = base64String;

                if (_repository.Add(post))
                {
                    var author = _userService.GetUserById(post.UserId);

                    if (author == null)
                    {
                        return false;
                    }
                    _emailService.SendEmailToSubscribers(entity, author.Fullname);

                    return true;
                }
                return false;
            }   
        }

        public bool Any(int id) => _repository.Any(id);
        public bool DeleteById(int id)
        {
            if (!_repository.Any(id)) return false;

            return _repository.DeleteById(id);
        }


        //  I wonder if this 1 liner will cause any issues 
        public ICollection<PostDto> GetAll() => _mapper.Map<ICollection<PostDto>>(_repository.GetAll().ToList());
        //  It wasn't the method's fault :]

        public List<PostDto> GetPaginatedPosts(int pageIndex, PostFilter filters)
        {
            var query = _table.AsQueryable();

            // Apply sorting
            if (filters.SortBy == "old")
            {
                query = query.OrderBy(b => b.PostingTime);
                return _mapper.Map < List < PostDto >> (_repository.GetPaginatedPosts(pageIndex, query));
            }
            else if (filters.SortBy == "new")
            {
                query = query.OrderByDescending(b => b.PostingTime);
                return _mapper.Map<List<PostDto>>(_repository.GetPaginatedPosts(pageIndex, query));

            }
            else if (filters.SortBy == "popular")
            {
                query = query.OrderByDescending(b => b.Stars.Count);
                return _mapper.Map<List<PostDto>>(_repository.GetPaginatedPosts(pageIndex, query));

            }
            if (!string.IsNullOrEmpty(filters.Tags))
            {
                var tagsArray = filters.Tags.Split(',');
                query = query.Where(p => tagsArray.All(tag => p.Tags.Contains(tag)));
                return _mapper.Map<List<PostDto>>(_repository.GetPaginatedPosts(pageIndex, query));

            }

            if (filters.Year.HasValue)
            {
                query = query.Where(p => p.PostingTime.Year == filters.Year.Value);
                return _mapper.Map<List<PostDto>>(_repository.GetPaginatedPosts(pageIndex, query));

            }

            if (filters.Month.HasValue)
            {
                query = query.Where(p => p.PostingTime.Month == filters.Month.Value);
                return _mapper.Map<List<PostDto>>(_repository.GetPaginatedPosts(pageIndex, query));

            }

           return _mapper.Map<List<PostDto>>(_repository.GetPaginatedPosts(pageIndex, query));
        }
        //{
        //    var posts = _repository.GetPaginatedPosts(pageIndex);
        //    var mappedPosts = _mapper.Map<List<PostDto>>(posts);
        //    return mappedPosts;
        //}

        public PostDetailsDto? GetById(int id) => _mapper.Map<PostDetailsDto>(_repository.GetById(id));

        public bool Update(PostCreateDto entity, int id)
        {
            var found = _repository.GetById(id);
            if(found != null)
            {
                found.Title = entity.Title;
                found.Text = entity.Text;
                found.Description = entity.Description;
                //found.UserId = entity.UserId; -   No, we can't change the author of the post dummy!
                //found.Image = entity.Image; -     And I'm not sure if we want to be able to change the image(too much work[maybe?])
                found.Tags = entity.Tags.GetPostTags();

                return _repository.Update(found);
            }
            return false;
        }
    }
}
