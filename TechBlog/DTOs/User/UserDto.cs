namespace DTOs.User
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsSubscribed { get; set; }

    }
}
