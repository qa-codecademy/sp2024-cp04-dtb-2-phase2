﻿ namespace Domain_Models
{
    public class Comment : Base
    {
        public Comment(string name, string text, int userId)
        {
            Name = name.Length < 2 ? "Anonymous"
                                   : name;
            Text = text;
            Date = DateTime.UtcNow;
            UserId = userId;
        }
        public User User { get; set; }
        public int UserId { get; set; } 
        public int PostId { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
    }
}
