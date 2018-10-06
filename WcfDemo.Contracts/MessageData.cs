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
        Success = 1,
        [EnumMember]
        ValidationError = 2,
        [EnumMember]
        InternalError = 3
    }

    [DataContract]
    public enum LegalForm
    {
        [EnumMember]
        Person = 1,
        [EnumMember]
        Company = 2
    }

    [DataContract]
    public enum ContactType
    {
        [EnumMember]
        Mobile = 1,
        [EnumMember]
        Fax = 2,
        [EnumMember]
        Email = 3,
        [EnumMember]
        OfficePhone = 4,
        [EnumMember]
        OfficeFax = 5,
        [EnumMember]
        OfficeEmail = 6
    }
}
