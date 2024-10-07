using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Image
{
    public class UploadImageDto
    {
        public int PostId { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
