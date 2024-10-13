using DTOs.Post;

namespace Services.Interfaces
{
    public interface IEmailService
    {
        public void SendEmailToSubscribers(PostCreateDto createdPost, string authorFullName);
    }
}
