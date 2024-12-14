using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.FilterDto
{
    public class PostFilter
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage ="Page index can't be lower than 1!")]
        public int PageIndex { get; set; }
        public string SortBy { get; set; } = "new"; // "old", "new", "popular"
        public List<string> Tags { get; set; } = new() { "" };  // ["tag1", "tag2"] - tags
        public int? Year { get; set; } = 0; // Year filter
        public int? Month { get; set; } = 0; // Month filter
    }
}
