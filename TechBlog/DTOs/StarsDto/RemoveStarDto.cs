using System.ComponentModel.DataAnnotations;

namespace DTOs.StarsDto
{
    public class RemoveStarDto
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int PostId { get; set; }
    }
}
