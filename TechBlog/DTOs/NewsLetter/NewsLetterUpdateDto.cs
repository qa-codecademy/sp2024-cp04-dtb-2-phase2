using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.NewsLetter
{
    public class NewsLetterUpdateDto
    {
        [Required]
        public string Email { get; set; }
        public string Tag { get; set; }
        public int? AuthorID { get; set; }
    }
}
