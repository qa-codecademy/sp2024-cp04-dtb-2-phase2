using Domain_Models.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain_Models
{
    public class Post : Base
    {
        public string Title { get; set; }
        public string Text {  get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public List<Star> Stars { get; set; }
        public List<Comment> Comments { get; set; }
        public DateTime PostingTime { get; set; }
        public string? ImageBase64 {  get; set; }
        public int ? ImageId { get; set; }
        public Image Image { get; set; }
        public string Tags { get; set; }

        //[NotMapped]
        //public List<Tag> TagList
        //{
        //    get => Tags.Split(',').Select(t => Enum.Parse<Tag>(t)).ToList();
        //    set => Tags = string.Join(",", value);
        //}
    }
}
