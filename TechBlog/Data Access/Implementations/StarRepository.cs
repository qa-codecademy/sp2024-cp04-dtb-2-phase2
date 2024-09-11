using Data_Access.Interfaces;
using Domain_Models;

namespace Data_Access.Implementations
{
    public class StarRepository : Repository<Star>, IStarRepository
    {
        public StarRepository(TechBlogDbContext context) : base(context)
        {
        }
    }
}
