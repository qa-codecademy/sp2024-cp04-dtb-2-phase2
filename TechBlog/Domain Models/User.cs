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
        public string FullName => $"{FirstName} {LastName}";
    }
}
