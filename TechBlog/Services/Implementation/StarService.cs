using Data_Access.Interfaces;
using Domain_Models;
using DTOs.StarsDto;
using Mappers;
using Services.Interfaces;

namespace Services.Implementation
{
    public class StarService : IStarService
    {
        private readonly IStarRepository _repository;
        public StarService(IStarRepository repository)
        {
            _repository = repository;
        }

        public void AddRating(CreateStarDto dto)
        {
            if (dto.UserId < 0 || dto.PostId < 0)
                throw new ArgumentOutOfRangeException(nameof(dto.UserId), nameof(dto.PostId), nameof(dto.Rating));
            else
            {
                var star = StarMapper.ToDomainModel(dto);
                _repository.Add(star);
            }
        }

        public void RemoveRating(RemoveStarDto dto)
        {
            if (dto.UserId< 0 || dto.PostId< 0)
                throw new ArgumentOutOfRangeException(nameof(dto.UserId), nameof(dto.PostId));
            else
            {
                var found = _repository.GetStarByUserAndPostId(dto.UserId, dto.PostId);
                if(found != null)
                {
                    _repository.DeleteById(found.Id); 
                }
            }
        }

        public void UpdateRating(CreateStarDto dto)
        {
            if (dto.UserId < 0 || dto.PostId < 0 || dto.Rating< 0)
                throw new ArgumentOutOfRangeException(nameof(dto.UserId), nameof(dto.PostId), nameof(dto.Rating));
            else
            {
                var found = _repository.GetStarByUserAndPostId(dto.UserId, dto.PostId);
                found.Rating = dto.Rating;
                _repository.Update(found);
            }
        }
        // OVA BI TREBALO VO POST SERVISOT DA ODI

        // Ke odi vo mapperot sega ( :
        public Star GetStarByUserAndPostId(RemoveStarDto dto)
        {
            var result = _repository.GetStarByUserAndPostId(dto.UserId, dto.PostId);
            return result;
        }
    }
}
