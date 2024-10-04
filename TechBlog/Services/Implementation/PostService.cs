using AutoMapper;
using Data_Access.Interfaces;
using Domain_Models;
using DTOs.Post;
using Mappers.MapperConfig;
using Services.Interfaces;

namespace Services.Implementation
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _repository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly IUserService _userService;
        public PostService(IPostRepository repo, IMapper mapper, IEmailService emailService, IUserService userService)
        {
            _repository = repo;
            _mapper = mapper;
            _emailService = emailService;
            _userService = userService;
        }
        public bool Add(PostCreateDto entity)
        {
            var post = _mapper.Map<Post>(entity);
            if (_repository.Add(post))
            {
                var author = _userService.GetUserById(post.Id);

                if (author == null) 
                {
                    return false;
                }
                _emailService.SendEmail(entity, author.Fullname);

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


        public ICollection<PostDto> GetAll() => _mapper.Map<ICollection<PostDto>>(_repository.GetAll().ToList());

        //  I wonder if this 1 liner will cause any issues 
        public List<PostDto> GetPaginatedPosts(int pageIndex) => _mapper.Map<List<PostDto>>(_repository.GetPaginatedPosts(pageIndex));
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
                found.UserId = entity.UserId;
                found.Image = entity.Image;
                found.Tags = entity.Tags.GetPostTags();

                return _repository.Update(found);
            }
            return false;
        }
    }
}
