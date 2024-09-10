using Domain_Models.Enums;

namespace Domain_Models
{
    public class Post : Base
    {
        public string Title { get; set; }
        public string Text {  get; set; }
        public List<Tag> Tags { get; set; }
        public int UserId { get; set; }
        public List<int> Stars { get; set; }
        public List<int> Comments { get; set; }
        public DateTime PostingTime { get; set; }

    }
}
