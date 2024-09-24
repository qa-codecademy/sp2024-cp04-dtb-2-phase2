﻿using System.ComponentModel.DataAnnotations;

namespace Domain_Models
{
    public class Star : Base
    {
        public int UserId { get; set; }
        public int PostId { get; set; }
        [Range(0,5)]
        public int Rating { get; set; }

        public User User { get; set; }
        //public Post Post { get; set; }
    }
}
