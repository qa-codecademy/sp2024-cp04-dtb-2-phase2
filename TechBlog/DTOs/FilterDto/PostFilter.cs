using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.FilterDto
{
    public class PostFilter
    {
        public string SortBy { get; set; } // "old", "new", "popular"
        public string Tags { get; set; }   // Comma-separated tags
        public int? Year { get; set; }     // Year filter
        public int? Month { get; set; }    // Month filter
    }
}
