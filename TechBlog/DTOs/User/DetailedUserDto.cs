using Domain_Models;

namespace DTOs.User
{
    public class DetailedUserDto
    {
        public string FullName => $"{FirstName} {LastName}";
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsSubscribed { get; set; }
        public bool IsAdmin { get; set; }
    }
}
