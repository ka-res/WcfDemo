using System.Linq;
using System.Net.Mail;
using System.Net;
using System;

namespace WcfDemo
{
    public static class SmtpClientHelper
    {
        public static MessageResponse SendMessage(MessageRequest messageRequest, out string mailBody)
        {
            var configurator = new ConfigHandler();

            const string subject = "WcfDemo Sample Header by ka_res";
            const string host = "smtp.gmail.com";

            mailBody = configurator.GetMailBody();

            var emailAddress = messageRequest.LegalForm == LegalForm.Person
                ? messageRequest.Contacts.Single(x => x?.ContactType == ContactType.Email).Value
                : messageRequest.Contacts.Single(x => x?.ContactType == ContactType.OfficeEmail).Value;
            
            var eMail = new MailMessage();
            eMail.From = new MailAddress(configurator.GetUserName());
            eMail.To.Add(new MailAddress(emailAddress));
            eMail.IsBodyHtml = true;
            eMail.Subject = subject;
            eMail.Body = mailBody;

            var smtpClient = new SmtpClient
            {
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(configurator.GetUserName(), configurator.GetPassword()),
                Host = host
            };

            try
            {
                smtpClient.Send(eMail);
            }
            catch (Exception)
            {
                return new MessageResponse
                {
                    ReturnCode = ReturnCode.InternalError,
                    ErrorMessage = "Wystąpił błąd związany z wysłaniem wiadomości za pomocą GMail"
                };
            }

            return new MessageResponse
            {
                ReturnCode = ReturnCode.Success,
                ErrorMessage = null
            };
        }
    }
}
