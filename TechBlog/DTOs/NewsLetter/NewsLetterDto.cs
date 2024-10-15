using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.NewsLetter
{
    public class NewsLetterDto
    {
        public string Email { get; set; }
        public List<string> Tags { get; set; }
        public List<string> Authors { get; set; }
    }
}
