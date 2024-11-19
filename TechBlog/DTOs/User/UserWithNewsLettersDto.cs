using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.User
{
    public class UserWithNewsLettersDto
    {
        public string FullName => $"{FirstName} {LastName}";
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsSubscribed { get; set; }
        public bool IsAdmin { get; set; }
        public NewsLetter.NewsLetterDto NewsLetter { get; set; }
    }
}
