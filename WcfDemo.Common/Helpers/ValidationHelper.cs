using System.Linq;
using System.Net.Mail;

namespace WcfDemo.Common.Helpers
{
    public class ValidationHelper
    {
        public static MessageResponse ValidateMessage(MessageRequest message)
        {
            MessageResponse messageResponse;
            switch (message.LegalForm)
            {
                case LegalForm.Person:
                    messageResponse = ValidateForPerson(message);
                    break;

                case LegalForm.Company:
                    messageResponse = ValidateForCompany(message);
                    break;

                default:
                    messageResponse = new MessageResponse
                    {
                        ReturnCode = ReturnCode.InternalError,
                        ErrorMessage = "Wystąpił problem z ustaleniem typu adresata (osoba/firma)."
                    };
                    break;
            }
            
            return messageResponse;
        }

        private static MessageResponse ValidateForPerson(MessageRequest message)
        {
            var personalData = ValidateForPersonalData(message);
            if (!string.IsNullOrEmpty(personalData))
            {
                return new MessageResponse
                {
                    ReturnCode = ReturnCode.ValidationError,
                    ErrorMessage = personalData
                };
            }

            var contactTypeQuantity = ValidateContactTypeQuantity(message, ContactType.Email);
            if (!string.IsNullOrEmpty(contactTypeQuantity))
            {
                return new MessageResponse
                {
                    ReturnCode = ReturnCode.ValidationError,
                    ErrorMessage = contactTypeQuantity
                };
            }

            var emailFormat = ValidateEmailFormat(message, ContactType.Email);
            if (!string.IsNullOrEmpty(emailFormat))
            {
                return new MessageResponse
                {
                    ReturnCode = ReturnCode.ValidationError,
                    ErrorMessage = emailFormat
                };
            }

            return new MessageResponse
            {
                ReturnCode = ReturnCode.Success,
                ErrorMessage = null
            };
        }

        private static MessageResponse ValidateForCompany(MessageRequest message)
        {
            var lastNamePresence = ValidateCompanyLastName(message);
            if (!string.IsNullOrEmpty(lastNamePresence))
            {
                return new MessageResponse
                {
                    ReturnCode = ReturnCode.ValidationError,
                    ErrorMessage = lastNamePresence
                };
            }

            var contactTypeQuantity = ValidateContactTypeQuantity(message, ContactType.OfficeEmail);
            if (!string.IsNullOrEmpty(contactTypeQuantity))
            {
                return new MessageResponse
                {
                    ReturnCode = ReturnCode.ValidationError,
                    ErrorMessage = contactTypeQuantity
                };
            }

            var emailFormat = ValidateEmailFormat(message, ContactType.OfficeEmail);
            if (!string.IsNullOrEmpty(emailFormat))
            {
                return new MessageResponse
                {
                    ReturnCode = ReturnCode.ValidationError,
                    ErrorMessage = emailFormat
                };
            }

            return new MessageResponse
            {
                ReturnCode = ReturnCode.Success,
                ErrorMessage = null
            };
        }

        private static string ValidateForPersonalData(MessageRequest message)
        {
            return !string.IsNullOrWhiteSpace(message.FirstName) && !string.IsNullOrWhiteSpace(message.LastName)
                ? null
                : $"Nie podano imienia lub nazwiska (imię: {message.FirstName}, nazwisko: {message.LastName})";
        }

        private static string ValidateContactTypeQuantity(MessageRequest message, ContactType contactType)
        {
            var chosenContactsCount = message.Contacts.Where(x => x.ContactType == contactType).Count();
            return chosenContactsCount == 1
                ? null
                : $"Nie istnieje dokładnie jeden wpis o rodzaju {contactType.ToString()} w kontaktach. Jest ich {chosenContactsCount}";
        }

        private static string ValidateEmailFormat(MessageRequest message, ContactType contactType)
        {
            var eMail = message.Contacts.Single(x => x.ContactType == contactType).Value;
            var address = new MailAddress(eMail);
            return address.Address == eMail
                ? null
                : $"Wprowadzono niepoprawny format adresu e-mail {eMail}";
        }

        private static string ValidateCompanyLastName(MessageRequest message)
        {
            return !string.IsNullOrWhiteSpace(message.LastName)
                ? null
                : $"Nie podano nazwy firmy";
        }
    }
}
