using Data_Access.Interfaces;
using DTOs.Post;
using Mappers;
using Services.Interfaces;

namespace Services.Implementation
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _repository;
        public PostService(IPostRepository repo)
        {
            _repository = repo;
        }
        public bool Add(PostCreateDto entity) => _repository.Add(entity.ToModel());

        public bool Any(int id) => _repository.Any(id);
        public bool DeleteById(int id)
        {
            if (!_repository.Any(id)) return false;

            return _repository.DeleteById(id);
        }

        public ICollection<PostDto> GetAll() => _repository.GetAll().Select(x => x.ToModel()).ToList();
        public PostDetailsDto? GetById(int id) => _repository.GetById(id).ToDetailedModel();

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
                //dto.Tags = model.Tags;

                return _repository.Update(found);
            }
            return false;
        }
    }
}
