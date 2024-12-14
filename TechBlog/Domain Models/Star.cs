using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain_Models
{
    public class Star : Base
    {
        public int UserId { get; set; }
        public int PostId { get; set; }
        [Range(0,5)]
        public int Rating { get; set; }

        [JsonIgnore]
        public User User { get; set; }
        //public Post Post { get; set; }
    }
}
