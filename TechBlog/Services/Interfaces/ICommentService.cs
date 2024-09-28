using DTOs.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ICommentService
    {
        ICollection<CommentDto> GetAll();
        CommentDto GetById(int id);
        void Add(AddCommentDto addCommentDto);
        void Delete(int id);
        void Update(UpdateCommentDto updateCommentDto);
    }
}
