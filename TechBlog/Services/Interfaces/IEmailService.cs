using DTOs.Post;

namespace Services.Interfaces
{
    public interface IEmailService
    {
        public void SendEmail(PostCreateDto createdPost, string authorFullName);
    }
}
