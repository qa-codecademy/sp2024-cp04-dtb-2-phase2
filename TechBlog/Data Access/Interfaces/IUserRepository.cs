using Domain_Models;

namespace Data_Access.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        User? GetUserByEmail(string email);
        User? GetUserByEmailAndPassword(string email, string password);
    }
}
