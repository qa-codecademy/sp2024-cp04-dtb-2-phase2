using Domain_Models;

namespace Mappers
{
    public static class StarMapper
    {
        public static Star ToDomainModel(int userId, int postId, int rating)
        {
            return new Star() { Id = userId, PostId = postId, Rating=RatingRangeCheck(rating) };
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
