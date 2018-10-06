using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WcfDemo
{
    [Table("MessageRequests")]
    public class MessageRequestModel : BaseModel
    {
        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public int LegalFormId { get; set; }
        [ForeignKey("LegalFormId")]
        public virtual LegalFormModel LegalForm { get; set; }

        [Required]
        public virtual ICollection<ContactModel> Contacts { get; set; }
    }
}