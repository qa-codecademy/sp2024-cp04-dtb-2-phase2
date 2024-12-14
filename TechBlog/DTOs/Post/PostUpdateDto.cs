using System.ComponentModel.DataAnnotations;

namespace DTOs.Post
{
    public class PostUpdateDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Text { get; set; }
        [Required]
        public string? Description { get; set; }
        //public int? UserId { get; set; }
        //public List<string> Tags { get; set; }
    }
}
