using Data_Access.Interfaces;
using Domain_Models;
using Microsoft.EntityFrameworkCore;

namespace Data_Access.Implementations
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        private readonly TechBlogDbContext _context;
        private readonly DbSet<Post> _table;
        public PostRepository(TechBlogDbContext context) : base(context)
        {
            _context = context;
            _table = _context.Set<Post>();
        }
        public async Task<PaginatedList> GetPaginatedPosts(int pageIndex)
        {
            var pageSize = 12;
            var posts = await _table
                .OrderBy(b => b.Id)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            var count = await _context.Posts.CountAsync();
            var totalPages = (int)Math.Ceiling(count / (double)pageSize);
            return new PaginatedList(posts, pageIndex, totalPages);
        }
    }
}
