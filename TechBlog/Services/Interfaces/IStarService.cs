using Domain_Models;
using DTOs.StarsDto;

namespace Services.Interfaces
{
    public interface IStarService
    {
        void AddRating(CreateStarDto dto);
        void RemoveRating(RemoveStarDto dto);
        void UpdateRating(CreateStarDto dto);
        Star GetStarByUserAndPostId (RemoveStarDto dto);

    }
}
