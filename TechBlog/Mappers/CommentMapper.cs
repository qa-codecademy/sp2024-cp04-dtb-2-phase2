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

            };
        }

        public static Comment ToComment(this AddCommentDto addComment)
        {
            return new Comment(addComment.Name, addComment.Text)
            {
                PostId = addComment.PostId

            };
        }


    }
}
