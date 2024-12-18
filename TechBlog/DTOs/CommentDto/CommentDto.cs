﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.CommentDto
{
    public class CommentDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public int PostId { get; set; }
        public DateTime Date { get; set; }
    }
}
