using DTOs.User;

namespace DTOs.NewsLetter
{
    public class NewsLetterDto
    {
        public string Email { get; set; }
        public List<string> Tags { get; set; }
        public List<UserDto> Authors { get; set; }
    }
}
