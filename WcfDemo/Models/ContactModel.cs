using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WcfDemo
{
    [Table("Contacts")]
    public class ContactModel : BaseModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ContactTypeId { get; set; }
        [ForeignKey("ContactTypeId")]
        public virtual ContactTypeModel ContactType { get; set; }

        [Required]
        public string Value { get; set; }

        [Required]
        public int MessageRequestId { get; set; }
        [ForeignKey("MessageRequestId")]
        public virtual MessageRequestModel MessageRequest { get; set; }
    }
}