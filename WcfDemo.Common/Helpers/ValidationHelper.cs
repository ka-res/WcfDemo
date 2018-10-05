using WcfDemo.Contracts;
using System.Linq;
using System.Net.Mail;

namespace WcfDemo.Common.Helpers
{
    public class ValidationHelper
    {
        public static MessageResponse ValidateMessage(MessageRequest message)
        {
            switch (message.LegalForm)
            {
                case LegalForm.Person:
                    ValidateForPerson(message);
                    break;

                case LegalForm.Company:
                    ValidateForCompany(message);
                    break;
            }

            //todo
            return new MessageResponse
            {
                ReturnCode = ReturnCode.InternalError,
                ErrorMessage = ""
            };
        }

        private static MessageResponse ValidateForPerson(MessageRequest message)
        {
            var personalData = ValidateForPersonalData(message);
            if (!personalData)
            {
                return new MessageResponse
                {
                    ReturnCode = ReturnCode.ValidationError,
                    ErrorMessage = ""
                };
            }

            var contactTypeQuantity = ValidateContactTypeQuantity(message, ContactType.Email);
            if (!contactTypeQuantity)
            {
                return new MessageResponse
                {
                    ReturnCode = ReturnCode.ValidationError,
                    ErrorMessage = ""
                };
            }

            var contactType = ValidateForContactType(message, ContactType.Email);
            if (!contactType)
            {
                return new MessageResponse
                {
                    ReturnCode = ReturnCode.ValidationError,
                    ErrorMessage = ""
                };
            }

            var emailFormat = ValidateEmailFormat(message, ContactType.Email);
            if (!emailFormat)
            {
                return new MessageResponse
                {
                    ReturnCode = ReturnCode.ValidationError,
                    ErrorMessage = ""
                };
            }

            return new MessageResponse
            {
                ReturnCode = ReturnCode.Success,
                ErrorMessage = ""
            };
        }

        private static MessageResponse ValidateForCompany(MessageRequest message)
        {
            var lastNamePresence = ValidateCompanyLastName(message);
            if (!lastNamePresence)
            {
                return new MessageResponse
                {
                    ReturnCode = ReturnCode.ValidationError,
                    ErrorMessage = ""
                };
            }

            var contactTypeQuantity = ValidateContactTypeQuantity(message, ContactType.OfficeEmail);
            if (!contactTypeQuantity)
            {
                return new MessageResponse
                {
                    ReturnCode = ReturnCode.ValidationError,
                    ErrorMessage = ""
                };
            }

            var contactType = ValidateForContactType(message, ContactType.OfficeEmail);
            if (!contactType)
            {
                return new MessageResponse
                {
                    ReturnCode = ReturnCode.ValidationError,
                    ErrorMessage = ""
                };
            }

            var emailFormat = ValidateEmailFormat(message, ContactType.OfficeEmail);
            if (!emailFormat)
            {
                return new MessageResponse
                {
                    ReturnCode = ReturnCode.ValidationError,
                    ErrorMessage = ""
                };
            }

            return new MessageResponse
            {
                ReturnCode = ReturnCode.Success,
                ErrorMessage = ""
            };
        }

        private static bool ValidateForPersonalData(MessageRequest message)
        {
            return !string.IsNullOrWhiteSpace(message.FirstName) && !string.IsNullOrWhiteSpace(message.LastName);
        }

        private static bool ValidateContactTypeQuantity(MessageRequest message, ContactType contactType)
        {
            return message.Contacts.Where(x => x.ContactType == contactType).Count() == 1;
        }

        private static bool ValidateForContactType(MessageRequest message, ContactType contactType)
        {
            return message.Contacts.All(x => x.ContactType != contactType);
        }

        private static bool ValidateEmailFormat(MessageRequest message, ContactType contactType)
        {
            var eMail = message.Contacts.Single(x => x.ContactType == contactType).Value;
            var address = new MailAddress(eMail);
            return address.Address == eMail;
        }

        private static bool ValidateCompanyLastName(MessageRequest message)
        {
            return !string.IsNullOrWhiteSpace(message.LastName);
        }
    }
}
