using Domain_Models;
using DTOs.FilterDto;

namespace Data_Access.Interfaces
{
    public interface IPostRepository : IRepository<Post>
    {
        public Task<PaginatedList> GetPaginatedPosts(int pageIndex, IQueryable<Post> query);
        public Post GetDetailedPost(int id);
    }
}
