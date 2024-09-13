using Domain_Models;

namespace Data_Access.Interfaces
{
    public interface IUserReposiotry : IRepository<User>
    {
        User GetUserByEmail(string email);
        User GetUserByEmailAndPassword(string email, string password);
    }
}
