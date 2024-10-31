using System.ComponentModel.DataAnnotations;

namespace DTOs.StarsDto
{
    public class CreateStarDto
    {
        [Required]
        [Range(1, 5, ErrorMessage ="Please Ensure to enter a rating from 1 to 5")]
        public int Rating { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int PostId { get; set; }
    }
}
