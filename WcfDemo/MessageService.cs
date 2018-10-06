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

            SendMessage(message, out messageResponse);
            SaveMessageData(message, messageResponse);

            return messageResponse;
        }

        private void SendMessage(MessageRequest message, out MessageResponse messageResponse)
        {
            try
            {
                var response = SmtpClientHelper.SendMessage(message);
                messageResponse = response;

            }
            catch (Exception e)
            {
                messageResponse = new MessageResponse
                {
                    ReturnCode = ReturnCode.InternalError,
                    ErrorMessage = $"Wystąpił błąd związany z dostarczeniem wiadomości e-mail{Environment.NewLine}{e}"
                };
            }
        }

        private void SaveMessageData(MessageRequest message, MessageResponse messageResponse)
        {
            var messageRequestModel = new MessageRequestModel()
            {
                FirstName = message.FirstName,
                LastName = message.LastName,
                IsSoftDeleted = false,
                LegalFormId = (int)message.LegalForm,
                SaveDate = DateTime.Now
            };

            messageRequestModel.Contacts = message.Contacts
                .Where(x => x != null)
                .Select(x => new ContactModel
                {
                    ContactTypeId = (int)x?.ContactType,
                    IsSoftDeleted = false,
                    MessageRequestId = messageRequestModel.Id,
                    SaveDate = DateTime.Now,
                    Value = x?.Value
                }).ToList();

            _messageRequestRepository.Add(messageRequestModel);

            foreach (var contact in message.Contacts)
            {

                var contactModel = new ContactModel
                {
                    ContactTypeId = (int)contact.ContactType,
                    IsSoftDeleted = false,
                    MessageRequestId = messageRequestModel.Id,
                    SaveDate = DateTime.Now,
                    Value = contact.Value
                };

                _contactRepository.Add(contactModel);
            }

            var messageResponseModel = new MessageResponseModel
            {
                ErrorMessage = messageResponse.ErrorMessage,
                IsSoftDeleted = false,
                MessageRequestId = messageRequestModel.Id,
                ReturnCodeId = (int)messageResponse.ReturnCode,
                SaveDate = DateTime.Now
            };

            _messageResponseRepository.Add(messageResponseModel);
        }
    }
}
