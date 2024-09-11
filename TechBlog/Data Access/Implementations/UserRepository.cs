using Data_Access.Interfaces;
using Domain_Models;

namespace Data_Access.Implementations
{
    public class UserRepository : Repository<User>, IUserReposiotry
    {
        public UserRepository(TechBlogDbContext context) : base(context)
        {
        }
    }
}
