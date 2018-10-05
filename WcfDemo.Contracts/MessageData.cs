using System.Runtime.Serialization;

namespace WcfDemo
{
    [DataContract]
    public class MessageRequest
    {
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public LegalForm LegalForm { get; set; }
        [DataMember]
        public Contact[] Contacts { get; set; }
    }

    [DataContract]
    public class Contact
    {
        [DataMember]
        public ContactType ContactType { get; set; }
        [DataMember]
        public string Value { get; set; }
    }

    [DataContract]
    public class MessageResponse
    {
        [DataMember]
        public ReturnCode ReturnCode { get; set; }
        [DataMember]
        public string ErrorMessage { get; set; }
    }

    [DataContract]
    public enum ReturnCode
    {
        [EnumMember]
        Success = 0,
        [EnumMember]
        ValidationError = 1,
        [EnumMember]
        InternalError = 2
    }

    [DataContract]
    public enum LegalForm
    {
        [EnumMember]
        Person = 0,
        [EnumMember]
        Company = 1
    }

    [DataContract]
    public enum ContactType
    {
        [EnumMember]
        Mobile = 0,
        [EnumMember]
        Fax = 1,
        [EnumMember]
        Email = 2,
        [EnumMember]
        OfficePhone = 3,
        [EnumMember]
        OfficeFax = 4,
        [EnumMember]
        OfficeEmail = 5
    }
}
