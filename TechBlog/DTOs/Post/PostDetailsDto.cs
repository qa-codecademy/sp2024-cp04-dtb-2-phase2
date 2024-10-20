using Domain_Models;
using DTOs.User;

namespace DTOs.Post
{
    public class PostDetailsDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public UserDto User { get; set; }
        public decimal Rating { get; set; }
        public List<CommentDto.CommentDto> Comments { get; set; }
        public DateTime PostingTime { get; set; }
        public string Image { get; set; }
        public List<string> Tags { get; set; }
    }
}
