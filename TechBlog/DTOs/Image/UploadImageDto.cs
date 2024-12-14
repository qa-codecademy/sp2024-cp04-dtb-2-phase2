using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Image
{
    public class UploadImageDto
    {
        public int? UserId { get; set; }
        [Required]
        public string ImageFile { get; set; }
    }
}
