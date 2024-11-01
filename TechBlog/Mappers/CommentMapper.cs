using Domain_Models;
using DTOs.CommentDto;

namespace Mappers
{
    public static class CommentMapper
    {
        public static CommentDto ToCommentDto(this Comment comment)
        {
            return new CommentDto
            {
                Name = comment.Name,
                Text = comment.Text,
                PostId = comment.PostId,
                Date = comment.Date,
                UserId = comment.UserId,

            };
        }

        public static Comment ToComment(this AddCommentDto addComment, int userId)
        {
            return new Comment(addComment.Name, addComment.Text, userId)
            {
                PostId = addComment.PostId,

            };
        }


    }
}
