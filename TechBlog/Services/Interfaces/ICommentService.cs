using DTOs.CommentDto;

namespace Services.Interfaces
{
    public interface ICommentService
    {
        ICollection<CommentDto> GetAll();
        CommentDto GetById(int id);
        void Add(AddCommentDto addCommentDto, int userId);
        void Delete(int id);
        void Update(UpdateCommentDto updateCommentDto);
    }
}
