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
        public int PageIndex { get; set; }
        public string SortBy { get; set; } // "old", "new", "popular"
        public List<string> Tags { get; set; }   // Comma-separated tags
        public int? Year { get; set; }     // Year filter
        public int? Month { get; set; }    // Month filter
    }
}
