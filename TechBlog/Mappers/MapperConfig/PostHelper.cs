using Domain_Models;
namespace Mappers.MapperConfig
{
    public static class PostHelper
    {
        public static decimal GetPostRating(this List<Star> stars)
        {
            if (stars.Count < 1) return 0;
            return stars.Sum(x => x.Rating) / stars.Count;
        }
        public static string GetPostTags(this List<string> tags) => string.Join(",", tags);
    }
}