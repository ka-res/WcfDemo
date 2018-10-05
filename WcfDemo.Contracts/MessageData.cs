using System.Runtime.Serialization;

namespace WcfDemo
{
    [DataContract]
    public class MessageRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public LegalForm LegalForm { get; set; }
        public Contact[] Contacts { get; set; }
    }

    [DataContract]
    public class Contact
    {
        public ContactType ContactType { get; set; }
        public string Value { get; set; }
    }

    [DataContract]
    public class MessageResponse
    {
        public ReturnCode ReturnCode { get; set; }
        public string ErrorMessage { get; set; }
    }

    [DataContract]
    public enum ReturnCode
    {
        Success = 0,
        ValidationError = 1,
        InternalError = 2
    }

    [DataContract]
    public enum LegalForm
    {
        Person = 0,
        Company = 1
    }

    [DataContract]
    public enum ContactType
    {
        Mobile = 0,
        Fax = 1,
        Email = 2,
        OfficePhone = 3,
        OfficeFax = 4,
        OfficeEmail = 5
    }
}
