namespace Domain_Models
{
    public class User : Base
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsSubscribed { get; set; }
        public List<Star> Stars { get; set; }
        public List<Post> Posts { get; set; }
        public List<Comment> Comments { get; set; } // added a list of comments
        public List<Image> Images { get; set; } 
        public string FullName => $"{FirstName} {LastName}";
        public bool IsAdmin { get; set; }
        public List<NewsLetter> NewsLetters { get; set; } = new List<NewsLetter>();
    }
}
