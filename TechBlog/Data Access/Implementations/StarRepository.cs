using Data_Access.Interfaces;
using Domain_Models;

namespace Data_Access.Implementations
{
    public class StarRepository : Repository<Star>, IStarRepository
    {
        private readonly TechBlogDbContext _context;
        public StarRepository(TechBlogDbContext context) : base(context)
        {
            _context = context;
        }
        public List<Star> GetAllStarsForPost(int postId) 
        {
            return _context.Stars.Where(x => x.PostId == postId).ToList();
        }
    }
}
