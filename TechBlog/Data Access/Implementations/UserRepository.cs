using Data_Access.Interfaces;
using Domain_Models;
using Microsoft.EntityFrameworkCore;

namespace Data_Access.Implementations
{
    public class UserRepository : Repository<User> , IUserReposiotry 
    {
        private TechBlogDbContext _dbContext;
        public UserRepository(TechBlogDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public User GetUserByEmail(string email)
        { 
            return _dbContext.Users.FirstOrDefault(x => x.Email == email);
        }

        public User GetUserByEmailAndPassword(string email, string password)
        {
            return _dbContext.Users.FirstOrDefault(x=> x.Email.ToLower() == email.ToLower() && x.Password == password);
        }

    }
}
