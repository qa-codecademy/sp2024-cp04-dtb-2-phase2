using Data_Access.Interfaces;
using Domain_Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Data_Access.Implementations
{
    public class NewsLetterRepository : INewsLetterRepository
    {
        private readonly TechBlogDbContext _context;
        private readonly DbSet<NewsLetter> _table;


        public NewsLetterRepository(TechBlogDbContext context)
            
        {
            _context = context;
            _table = _context.Set<NewsLetter>();
        }

        public bool Any(string email) => _table.Any(x => x.Email.Equals(email));

        public bool Delete(string email)
        {
            if(Any(email))
            {
                var found = GetByEmail(email);
                _table.Remove(found);
                return  _context.SaveChanges() > 0;
            };
            return false;
        }

        public NewsLetter GetByEmail(string email)
        {
            return _table.Include(x => x.Authors).FirstOrDefault(x => x.Email == email);
        }

        //public IEnumerable<NewsLetter> GetSubscribers(string author, List<string> tags)
        //{
        //    var newsLetters = _context.NewsLetterUsers.AsEnumerable();

        //    if (!newsLetters.Any())
        //        return newsLetters;

        //    //Filter subscribers based on preferences or no preferences
        //    var filteredNewsLetters = newsLetters
        //        .Where(n =>
        //            (n.Authors.IsNullOrEmpty() && n.Tags.IsNullOrEmpty()) || // No preferences
        //            ( // Matching either the author or tags
        //                (!n.Authors.IsNullOrEmpty() && n.Authors.Split(',')
        //                    .Any(a => a.Trim().Contains(author, StringComparison.OrdinalIgnoreCase))) ||
        //                (!n.Tags.IsNullOrEmpty() && n.Tags.Split(',')
        //                    .Any(t => tags.Contains(t.Trim())))
        //            )
        //        );

        //    return filteredNewsLetters;
        //}
        public IEnumerable<NewsLetter> GetSubscribers(int authorId, List<string> tags)
        {
            // Query NewsLetters including the related Users (Authors)
            var newsLetters = _context.NewsLetterUsers
                .Include(n => n.Authors) // Load the related authors
                .AsEnumerable();

            if (!newsLetters.Any())
                return newsLetters;

            // Filter subscribers based on preferences or no preferences
            var filteredNewsLetters = newsLetters
                .Where(n =>
                    (!n.Authors.Any() && string.IsNullOrEmpty(n.Tags)) || // No preferences
                    ( // Matching either the authorId or tags
                        (n.Authors.Any(a => a.Id == authorId)) || // Match author by Id
                        (!string.IsNullOrEmpty(n.Tags) && n.Tags.Split(',')
                            .Any(t => tags.Contains(t.Trim(), StringComparer.OrdinalIgnoreCase))) // Match tags
                    )
                );

            return filteredNewsLetters;
        }

        public bool Update(NewsLetter entity)
        {
            _table.Update(entity);
            return _context.SaveChanges() > 0;
        }

        public bool Add(NewsLetter entity)
        {
            _table.Add(entity);
            return _context.SaveChanges() > 0;
        }
        //  Maybe

        //public IEnumerable<NewsLetter> GetSubscribers(string author, List<string> tags)
        //{
        //    return _context.NewsLetterUsers
        //        .Where(n =>
        //            (n.Authors.IsNullOrEmpty() && n.Tags.IsNullOrEmpty()) ||  // Subscribers with no preferences
        //            (n.Authors.Split(',').Any(a => a.Trim().Contains(author, StringComparison.OrdinalIgnoreCase)) ||
        //             n.Tags.Split(',').Any(t => tags.Contains(t.Trim())))
        //        )
        //        .ToList();
        //}


    }
}
