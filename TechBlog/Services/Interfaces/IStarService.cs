namespace Services.Interfaces
{
    public interface IStarService
    {
        void AddRating(int userId, int postId, int rating);
        void RemoveRating(int userId, int postId);
        void UpdateRating(int userId, int postId, int rating);

    }
}
