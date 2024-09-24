using System.ComponentModel.DataAnnotations;

namespace DTOs.Post
{
    public class PostCreateDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public List<string> Tags { get; set; }
    }
}
