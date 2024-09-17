using Domain_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Post
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Rating { get; set; }
        public DateTime PostingTime { get; set; }
        public string Image { get; set; }
        public List<string> Tags { get; set; }
    }
}
