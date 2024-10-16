using Domain_Models;
using Domain_Models.Enums;

namespace Data_Access.Interfaces
{
    public interface INewsLetterRepository : IRepository<NewsLetter>
    {
        public IEnumerable<NewsLetter> GetSubscribers(int authorId, List<string> tags);
        public NewsLetter GetByEmail(string email);
    }
}
