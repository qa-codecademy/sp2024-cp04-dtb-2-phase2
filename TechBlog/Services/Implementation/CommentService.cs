using Data_Access.Implementations;
using Data_Access.Interfaces;
using Domain_Models;
using DTOs.Comment;
using Mappers;
using Services.Interfaces;
using Shared.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementation
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public void Add(AddCommentDto addCommentDto)
        {
            if (addCommentDto == null)
            {
                throw new DataException("User cannot be null");
            }

            var comment = CommentMapper.ToComment(addCommentDto);

            _commentRepository.Add(comment);

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
                throw new NotFoundException($"User with {id} not found");
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
