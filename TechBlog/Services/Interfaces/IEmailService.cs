using Domain_Models;
using DTOs.NewsLetter;
using DTOs.Post;

namespace Services.Interfaces
{
    public interface IEmailService
    {
        public void SendEmailToSubscribers(PostCreateDto createdPost);
        public NewsLetterDto GetSubscriberByEmail(string email);
        public bool Subscribe(string email);
        public bool Unsubscribe(string email);
        public bool UpdateSubscriber(NewsLetterUpdateDto subscriber);

    }
}
