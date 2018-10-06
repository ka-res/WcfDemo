using System;
using System.Linq;
using WcfDemo.Contracts;

namespace WcfDemo
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRequestRepository _messageRequestRepository;
        private readonly IMessageResponseRepository _messageResponseRepository;
        private readonly IContactRepository _contactRepository;

        public MessageService(
            IMessageRequestRepository messageRequestRepository,
            IMessageResponseRepository messageResponseRepository,
            IContactRepository contactRepository)
        {
            _messageRequestRepository = messageRequestRepository;
            _messageResponseRepository = messageResponseRepository;
            _contactRepository = contactRepository;
        }

        public MessageResponse Send(MessageRequest message)
        {
            var messageResponse = ValidationHelper.ValidateMessage(message);
            if (messageResponse.ReturnCode != ReturnCode.Success)
            {
                return messageResponse;
            }

            SendMessage(message, out var mailBody, out messageResponse);

            if (messageResponse.ReturnCode != ReturnCode.Success)
            {
                return messageResponse;
            }
            else
            {
                SaveMessageData(message, mailBody, messageResponse);
            }

            return messageResponse;
        }

        private void SendMessage(MessageRequest message, out string mailBody, out MessageResponse messageResponse)
        {
            mailBody = string.Empty;
            try
            {
                var response = SmtpClientHelper.SendMessage(message, out var mailBodyFromFile);
                messageResponse = response;
                mailBody = mailBodyFromFile;

            }
            catch (Exception)
            {
                messageResponse = new MessageResponse
                {
                    ReturnCode = ReturnCode.InternalError,
                    ErrorMessage = $"Wystąpił błąd związany z połączeniem z usługą Gmail"
                };
            }
        }

        private void SaveMessageData(MessageRequest message, string mailBody, MessageResponse messageResponse)
        {
            var messageRequestModel = new MessageRequestModel()
            {
                FirstName = message.FirstName,
                LastName = message.LastName,
                MailBody = mailBody,
                LegalFormId = (int)message.LegalForm
            };

            messageRequestModel.Contacts = message.Contacts
                .Where(x => x != null)
                .Select(x => new ContactModel
                {
                    ContactTypeId = (int)x?.ContactType,
                    MessageRequestId = messageRequestModel.Id,
                    Value = x?.Value
                }).ToList();

            _messageRequestRepository.Add(messageRequestModel);

            foreach (var contact in message.Contacts)
            {

                var contactModel = new ContactModel
                {
                    ContactTypeId = (int)contact.ContactType,
                    MessageRequestId = messageRequestModel.Id,
                    Value = contact.Value
                };

                _contactRepository.Add(contactModel);
            }

            var messageResponseModel = new MessageResponseModel
            {
                ErrorMessage = messageResponse.ErrorMessage,
                MessageRequestId = messageRequestModel.Id,
                ReturnCodeId = (int)messageResponse.ReturnCode
            };

            _messageResponseRepository.Add(messageResponseModel);
        }
    }
}
