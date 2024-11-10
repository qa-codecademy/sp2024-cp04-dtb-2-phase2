using Domain_Models;
using DTOs.FilterDto;
using DTOs.Post;

namespace Data_Access.Interfaces
{
    public interface IPostRepository : IRepository<Post>
    {
        public Task<PaginatedList> GetPaginatedPosts(int pageIndex, IQueryable<Post> query);
        public Post GetDetailedPost(int id);
        public List<Post> GetUserPosts(int id);
        public List<Post> GetAllPostsIncludingUsers();
        public List<Post> SearchPosts(string query);

    }
}
