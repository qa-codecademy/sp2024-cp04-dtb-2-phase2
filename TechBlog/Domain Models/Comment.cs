﻿ namespace Domain_Models
{
    public class Comment : Base
    {
        public Comment(string name, string text)
        {
            Name = name.Length < 2 ? "Anonymous"
                                   : name;
            Text = text;
            Date = DateTime.UtcNow;
        }
        public int PostId { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public int Id { get; set; }
    }
}
