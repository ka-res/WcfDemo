using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WcfDemo
{
    [Table("MessageResponses")]
    public class MessageResponseModel : BaseModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ReturnCodeId { get; set; }
        [ForeignKey("ReturnCodeId")]
        public virtual ReturnCodeModel ReturnCode { get; set; }

        public string ErrorMessage { get; set; }

        [Required]
        public int MessageRequestId { get; set; }
        [ForeignKey("MessageRequestId")]
        public virtual MessageResponseModel MessageRequest { get; set; }
    }
}