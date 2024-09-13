using Data_Access.Interfaces;
using Domain_Models;
using Microsoft.EntityFrameworkCore;

namespace Data_Access.Implementations
{
    public class UserRepository : IUserReposiotry
    {
        private TechBlogDbContext _dbContext;
        public UserRepository(TechBlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public void Add(User entity)
        {
            _dbContext.Users.Add(entity);
            _dbContext.SaveChanges();
        }

        public bool Any(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(User entity)
        {
            _dbContext.Users.Remove(entity);
            _dbContext.SaveChanges();
        }

        public void DeleteById(int id)
        {
             var deleteUser = _dbContext.Users.Find(id);

            if (deleteUser != null)
            {
                _dbContext.Users.Remove(deleteUser);
                _dbContext.SaveChanges();
            }
            else
            {
                throw new Exception($"User with Id {id} is not found");
            }
        }

        public List<User> GetAll()
        {
            return _dbContext.Users.ToList();
        }

        public User GetById(int id)
        {
            return _dbContext.Users.FirstOrDefault(x => x.Id == id);
        }

        public User GetUserByEmail(string email)
        { 
            return _dbContext.Users.FirstOrDefault(x => x.Email == email);
        }

        public User GetUserByEmailAndPassword(string email, string password)
        {
            return _dbContext.Users.FirstOrDefault(x=> x.Email.ToLower() == email.ToLower() && x.Password == password);
        }

        public void Update(User entity)
        {
            _dbContext.Users.Update(entity);
            _dbContext.SaveChanges();
        }

       
    }
}
