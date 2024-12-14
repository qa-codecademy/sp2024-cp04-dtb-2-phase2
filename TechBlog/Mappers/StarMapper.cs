using Domain_Models;
using DTOs.StarsDto;

namespace Mappers
{
    public static class StarMapper
    {
        public static Star ToDomainModel(CreateStarDto dto)
        {
            return new Star() { UserId = dto.UserId, PostId = dto.PostId, Rating = dto.Rating};
        }
        public static Star ToDomainModel(RemoveStarDto dto)
        {
            return new Star() { UserId = dto.UserId, PostId = dto.PostId};
        }
        public static int RatingRangeCheck ( int userRating)
        {
            if ( userRating < 0 ) 
                throw new ArgumentOutOfRangeException("The rating must be positive integer!");
            if (userRating > 5)
                throw new ArgumentOutOfRangeException("The rating must be in range [1] -> [5].");
            return userRating;
        }
    }
}
