using Domain_Models;
namespace Helpers
{
    public static class PostHelper
    {
        public static decimal GetPostRating(this List<Star> stars)
        {
            if (stars.Count < 1) return 0;
            return stars.Sum(x => x.Rating) / stars.Count;
        }
    }
}