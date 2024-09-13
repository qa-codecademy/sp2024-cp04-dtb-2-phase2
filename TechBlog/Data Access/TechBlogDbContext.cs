using Domain_Models;
using Domain_Models.Enums;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Star>()
                .HasOne(m => m.User)  
                .WithMany(u => u.Stars)  
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<Star>()
            //    .HasOne(m => m.Post)
            //    .WithMany(u => u.Stars)
            //    .HasForeignKey(m => m.UserId)
            //    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasMany(x => x.Stars)
                .WithOne(u => u.User)
                .HasForeignKey(i  => i.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
               .HasMany(x => x.Posts)
               .WithOne(u => u.User)
               .HasForeignKey(i => i.UserId)
               .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
