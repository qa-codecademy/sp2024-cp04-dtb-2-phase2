using Data_Access.Interfaces;
using Domain_Models;

namespace Data_Access.Implementations
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        private TechBlogDbContext _commentContext;
        public CommentRepository(TechBlogDbContext commentContext) : base(commentContext)
        {
            _commentContext = commentContext;
        }
    }
}
