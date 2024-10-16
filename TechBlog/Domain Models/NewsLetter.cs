namespace Domain_Models
{
    public class NewsLetter : Base
    {
        public string Email { get; set; }
        public string? Tags { get; set; }
        public List<User> Authors { get; set; }
    }
}
