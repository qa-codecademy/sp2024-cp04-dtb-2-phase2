using Domain_Models;

namespace Data_Access.Interfaces
{
    public interface IStarRepository : IRepository<Star>
    {
        List<Star> GetAllStarsForPost(int postId);
        Star GetStarByUserAndPostId(int userId, int postId);
    }
}
