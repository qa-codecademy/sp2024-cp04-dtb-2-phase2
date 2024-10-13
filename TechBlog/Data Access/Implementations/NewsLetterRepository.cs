using Data_Access.Interfaces;
using Domain_Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Data_Access.Implementations
{
    public class NewsLetterRepository : Repository<NewsLetter>, INewsLetterRepository
    {
        private readonly TechBlogDbContext _context;

        public NewsLetterRepository(TechBlogDbContext context)
            : base(context)
        {
            _context = context;
        }

        public IEnumerable<NewsLetter> GetSubscribers(string author, List<string> tags)
        {
            var newsLetters = _context.NewsLetterUsers.AsEnumerable();

            if (!newsLetters.Any())
                return newsLetters;

            // Filter subscribers based on preferences or no preferences
            var filteredNewsLetters = newsLetters
                .Where(n =>
                    (n.Authors.IsNullOrEmpty() && n.Tags.IsNullOrEmpty()) || // No preferences
                    ( // Matching either the author or tags
                        (!n.Authors.IsNullOrEmpty() && n.Authors.Split(',')
                            .Any(a => a.Trim().Contains(author, StringComparison.OrdinalIgnoreCase))) ||
                        (!n.Tags.IsNullOrEmpty() && n.Tags.Split(',')
                            .Any(t => tags.Contains(t.Trim())))
                    )
                );

            return filteredNewsLetters;
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
