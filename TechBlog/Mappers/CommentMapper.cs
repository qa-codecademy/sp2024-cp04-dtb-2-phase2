using Domain_Models;
using DTOs.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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
