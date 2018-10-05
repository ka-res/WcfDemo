using MimeKit;
using MailKit.Net.Smtp;
using System.Linq;

namespace WcfDemo
{
    public static class SmtpClientHelper
    {
        public static MessageResponse SendMessage(MessageRequest messageRequest)
        {
            const string userName = "WcfDemoUser";
            const string subject = "WcfDemo Sample Header";
            var configurator = new ConfigHandler();

            var emailAddress = messageRequest.LegalForm == LegalForm.Person
                ? messageRequest.Contacts.Single(x => x.ContactType == ContactType.Email).Value
                : messageRequest.Contacts.Single(x => x.ContactType == ContactType.OfficeEmail).Value;
            var name = messageRequest.LegalForm == LegalForm.Person
                ? $"{messageRequest.FirstName} {messageRequest.LastName}"
                : messageRequest.LastName;

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(userName, configurator.GetUserName()));
            message.To.Add(new MailboxAddress(name, emailAddress));
            message.Subject = subject;
            var builder = new BodyBuilder
            {
                TextBody = "This message was sent in order to test WCF service ability to work"
            };
            message.Body = builder.ToMessageBody();
            
            var client = new SmtpClient();
            client.Connect("smtp.gmail.com", 465, true);
            client.Authenticate(configurator.GetUserName(), configurator.GetPassword());
            client.Send(message);
            client.Disconnect(true);

            return new MessageResponse
            {
                ReturnCode = ReturnCode.Success,
                ErrorMessage = ""
            };
        }
    }
}
