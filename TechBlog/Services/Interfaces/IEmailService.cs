using Domain_Models;
using DTOs.NewsLetter;
using DTOs.Post;

namespace Services.Interfaces
{
    public interface IEmailService
    {
        public void SendEmailToSubscribers(PostCreateDto createdPost);
        public NewsLetterDto GetSubscriberByEmail(string email);
        public void Subscribe(string email);
        public void Unsubscribe(string email);
        public void UpdateSubscriber(NewsLetterUpdateDto subscriber);

    }
}
