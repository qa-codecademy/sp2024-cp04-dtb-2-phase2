using Domain_Models;
using DTOs.FilterDto;
using DTOs.Post;

namespace Services.Interfaces
{
    public interface IPostService
    {
        ICollection<PostDto> GetAll();
        PostDetailsDto? GetById(int id);
        bool Add(PostCreateDto entity);
        bool Any(int id);
        bool Update(PostUpdateDto entity);
        public Task<PaginatedListDto> GetPaginatedPosts(int pageIndex, PostFilter filters);
        //bool Delete(PostDto entity);
        bool DeleteById(int id);
        public List<PostDto> GetUserPosts(int userId);
    }
}
