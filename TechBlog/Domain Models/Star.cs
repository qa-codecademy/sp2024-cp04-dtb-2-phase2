namespace Domain_Models
{
    public class Star : Base
    {
        public int UserId { get; set; }
        public int PostId { get; set; }

        public User User { get; set; }
        //public Post Post { get; set; }
    }
}
