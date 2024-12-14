using Domain_Models;
using System.ComponentModel.DataAnnotations;

namespace DTOs.User
{
    public class UpdateUserDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        //public string? Password { get; set; }
    }
}
