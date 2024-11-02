using AutoMapper;
using Data_Access.Interfaces;
using Domain_Models;
using DTOs.CommentDto;
using Mappers;
using Services.Interfaces;
using Shared.CustomExceptions;

namespace Services.Implementation
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public CommentService(ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        public CommentResponseDto Add(AddCommentDto addCommentDto, int userId)
        {
            if (addCommentDto == null)
            {
                throw new DataException("User cannot be null");
            }

            var comment = CommentMapper.ToComment(addCommentDto, userId);

            _commentRepository.Add(comment);

            return _mapper.Map<CommentResponseDto>(comment);

        }

        public void Delete(int id)
        {
            Comment commentDb = _commentRepository.GetById(id);
            if (commentDb == null)
            {
                throw new NotFoundException("Comment not found");
            }
            _commentRepository.DeleteById(id);
        }

        public ICollection<CommentDto> GetAll()
        {
            return _commentRepository.GetAll().Select(x => x.ToCommentDto()).ToList();
        }
        public CommentDto GetById(int id)
        {
            Comment comment = _commentRepository.GetById(id);
            if (comment == null)
            {
                throw new NotFoundException($"Comment with id: {id} was not found");
            }
            CommentDto commentDto = comment.ToCommentDto();
            return commentDto;
        }

        public void Update(UpdateCommentDto updateCommentDto)
        {
            if (updateCommentDto == null)
            {
                throw new NotFoundException("Comment not found");
            }
            Comment commentDb = _commentRepository.GetById(updateCommentDto.Id);
            if (commentDb == null)
            {
                throw new NotFoundException($"Comment with id:{updateCommentDto.Id} is not found");
            }
            if (string.IsNullOrEmpty(updateCommentDto.Text))
            {
                throw new DataException($"Text can not be empty");
            }

            commentDb.Text = updateCommentDto.Text;

            _commentRepository.Update(commentDb);
        }
    }
}
