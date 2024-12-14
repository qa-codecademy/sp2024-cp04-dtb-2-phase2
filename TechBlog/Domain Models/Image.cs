using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Models
{
    public class Image : Base 
    {
        
        public int UserId { get; set; }

        public string Data { get; set; }
        //public string ContentType { get; set; }
        //public string Name { get; set; }

    }
}
