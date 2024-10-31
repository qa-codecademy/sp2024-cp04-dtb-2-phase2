using Data_Access.Interfaces;
using Domain_Models;
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

        public void AddRating(int userId, int postId, int rating)
        {
            if (userId < 0 || postId < 0)
                throw new ArgumentOutOfRangeException(nameof(userId), nameof(postId), nameof(rating));
            else
            {
                var star = StarMapper.ToDomainModel(userId, postId, rating);
                _repository.Add(star);
            }
        }

        public void RemoveRating(int userId, int postId)
        {
            if (userId < 0 || postId < 0)
                throw new ArgumentOutOfRangeException(nameof(userId), nameof(postId));
            else
            {
                var star = StarMapper.ToDomainModel(userId, postId, 1);
                //_repository.DeleteById(star); Tuka treba Id na svezdata taka?
            }
        }

        public void UpdateRating(int userId, int postId,int rating)
        {
            if (userId < 0 || postId < 0 || rating < 0)
                throw new ArgumentOutOfRangeException(nameof(userId), nameof(postId), nameof(rating));
            else
            {
                var star = StarMapper.ToDomainModel(userId,postId, rating);
                _repository.Update(star);
            }
        }
        // OVA BI TREBALO VO POST SERVISOT DA ODI

        // Ke odi vo mapperot sega ( :
        public List<Star> GetStarsByPostId(int postId)
        {
            var result =  _repository.GetAllStarsForPost(postId);
            if (result.Count < 0)
                return new List<Star>();

            return result;

        }
    }
}
