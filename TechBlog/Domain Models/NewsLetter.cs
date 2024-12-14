namespace Domain_Models
{// NewsLetterUsers is the name of the table in the DB also in the DBContext
    public class NewsLetter //: Base
    {
        public string Email { get; set; }
        public string? Tags { get; set; }
        public List<User> Authors { get; set; }
    }
}
    