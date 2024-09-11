using Domain_Models;
using Microsoft.EntityFrameworkCore;

namespace Data_Access
{
    public class TechBlogDbContext : DbContext
    {
        public TechBlogDbContext(DbContextOptions options)
            : base(options) { }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Star> Stars { get; set; }
        
    }
}
