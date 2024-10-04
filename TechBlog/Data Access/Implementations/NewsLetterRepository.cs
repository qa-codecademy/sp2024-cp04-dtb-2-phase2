using Data_Access.Interfaces;
using Domain_Models;
using Microsoft.EntityFrameworkCore;

namespace Data_Access.Implementations
{
    public class NewsLetterRepository : Repository<NewsLetter>, INewsLetterRepository
    {
        private readonly TechBlogDbContext _context;
        private readonly DbSet<NewsLetter> _table;
        public NewsLetterRepository(TechBlogDbContext context, DbSet<NewsLetter> table)
            : base(context)
        {
            _context = context;
            _table = table;
        }

        public IEnumerable<NewsLetter> FilterEmailsByAuthorAndTags(string author, List<string> tags)
        {
            // First, retrieve all newsletters from the database
            var newsLetters = _table.ToList();

            // Then, apply the filtering in memory
            var filteredNewsLetters = newsLetters
                .Where(n =>
                    n.Authors.Split(',')
                             .Any(a => a.Trim().Contains(author)) || // Check if any author contains the input author
                    n.Tags.Split(',')
                          .Any(t => tags.Contains(t.Trim())) // Check for matching tags
                );

            return filteredNewsLetters;
        }
    }
}
// an expression tree may not contain a call invocation that uses optional arguments