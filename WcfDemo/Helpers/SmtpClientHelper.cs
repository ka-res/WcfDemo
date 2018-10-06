using System.Linq;
using System.Net.Mail;
using System.Net;
using System;

namespace WcfDemo
{
    public static class SmtpClientHelper
    {
        public static MessageResponse SendMessage(MessageRequest messageRequest)
        {
            const string subject = "WcfDemo Sample Header";
            const string body = "To wiadomość testowa wysłana za pomocą serwisu WcfDemo (ka-res, 2018)";
            const string host = "smtp.gmail.com";

            var configurator = new ConfigHandler();

            var emailAddress = messageRequest.LegalForm == LegalForm.Person
                ? messageRequest.Contacts.Single(x => x?.ContactType == ContactType.Email).Value
                : messageRequest.Contacts.Single(x => x?.ContactType == ContactType.OfficeEmail).Value;
            
            var eMail = new MailMessage();
            eMail.From = new MailAddress(configurator.GetUserName());
            eMail.To.Add(new MailAddress(emailAddress));
            eMail.IsBodyHtml = true;
            eMail.Subject = subject;
            eMail.Body = body;

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
                ErrorMessage = "Wiadomośc została pomyślnie wysłana"
            };
        }
    }
}
