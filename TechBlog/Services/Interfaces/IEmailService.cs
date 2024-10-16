using DTOs.NewsLetter;
using DTOs.Post;

namespace Services.Interfaces
{
    public interface IEmailService
    {
        public void SendEmailToSubscribers(PostCreateDto createdPost);
        public void Subscribe(string email);
        public void Unsubscribe(string email);
        public void UpdateSubscriber(NewsLetterUpdateDto subscriber);

    }
}
