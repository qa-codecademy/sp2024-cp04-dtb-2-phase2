using AutoMapper;
using Data_Access.Interfaces;
using Domain_Models;
using DTOs.NewsLetter;
using DTOs.Post;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using MimeKit;
using Services.Interfaces;

namespace Services.Implementation
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        private readonly INewsLetterRepository _newsletterRepository;
        private readonly IUserRepository _userRepository;
        private IMapper _mapper { get; set; }
        public EmailService(IConfiguration config, INewsLetterRepository repo, IUserRepository userRepository, IMapper mapper)
        {
            _config = config;
            _newsletterRepository = repo;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public bool Subscribe(string email)
        {
            var subscriber = _newsletterRepository.GetByEmail(email);
            if (subscriber == null)
            {
                var user = _userRepository.GetUserByEmail(email);
                if (user != null)
                {
                    user.IsSubscribed = true;
                    _userRepository.Update(user);
                }
               return _newsletterRepository.Add(new NewsLetter() { Email = email });
            }
            return false;
        }
        public bool Unsubscribe(string email)
        {
            var subscriber = _newsletterRepository.GetByEmail(email);
            
            if (subscriber != null)
            {
                var user = _userRepository.GetUserByEmail(email);
                if (user != null)
                {
                    user.IsSubscribed = false;
                    _userRepository.Update(user);
                }

                return _newsletterRepository.Delete(subscriber.Email);
            }
            return false;
        }

        public void UpdateSubscriber(NewsLetterUpdateDto subscriber)
        {
            var found = _newsletterRepository.GetByEmail(subscriber.Email);
            if (found != null)
            {

                if (subscriber.AuthorID.HasValue)
                {
                    var foundAuthor = _userRepository.GetById(subscriber.AuthorID.Value);
                    if (foundAuthor != null)
                    {
                        found.Authors.Add(foundAuthor);
                    }
                }


                if (!string.IsNullOrEmpty(subscriber.Tag))
                {
                    if (string.IsNullOrEmpty(found.Tags))
                    {
                        List<string> tagsList = new()
                        {
                            subscriber.Tag
                        };
                        var updatedTags = string.Join(",", tagsList);
                        found.Tags = updatedTags;
                    } else {
                        var foundTagsList = found.Tags.Split(',').ToList();
                        foundTagsList.Add(subscriber.Tag);
                        found.Tags = string.Join(',', foundTagsList);
                    }
                }
                //var copyFound = new NewsLetter()
                //{
                //    Email = found.Email,
                //    Authors = found.Authors,
                //    Tags = found.Tags
                //};
                _newsletterRepository.Update(found);

            }
        }


        public void SendEmailToSubscribers(PostCreateDto createdPost)
        {
            var filteredEmails = _newsletterRepository.GetSubscribers(createdPost.UserId.Value, createdPost.Tags).ToList();

            int port = int.Parse(_config["EmailPort"]);
            InternetAddressList emailList = new();

            foreach (NewsLetter oneMail in filteredEmails)
            {
                emailList.Add(MailboxAddress.Parse(oneMail.Email));
            }

            //var request = new EmailObj() { };
            //var userDb = await _userRepository.GetUserByEmail(dBemail);
            //if (userDb == null)
            //{
            //    throw new DataException($"There is no user with this email {dBemail}.");
            //}

            var user = _userRepository.GetById(createdPost.UserId.Value);

            string input = String.Format($"The author {user.FullName} created a post,\n the title of the post is \"{createdPost.Title}\" containing the tags\n{createdPost.Tags}");

            //string mailstring = "Blah blah blah blah. Click <a href=\"http://127.0.0.1:5500/src/index.html\">here</a> for more information.";
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config["EmailUsername"]));
            email.To.AddRange(emailList);
            email.Subject = "New post notification";
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = input };

            using var smpt = new MailKit.Net.Smtp.SmtpClient(); // mailkit.net.smpt

            smpt.Connect(_config["EmailHost"], port, SecureSocketOptions.SslOnConnect);
            smpt.Authenticate(_config["EmailUsername"], _config["EmailPassword"]);
            smpt.Send(email);
            smpt.Disconnect(true);
        }

        public NewsLetterDto GetSubscriberByEmail(string email)
        {
            var found = _newsletterRepository.GetByEmail(email);
            return _mapper.Map<NewsLetterDto>(found);
        }
    }
}
