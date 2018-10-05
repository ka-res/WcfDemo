using System.ComponentModel.DataAnnotations;

namespace WcfDemo
{
    public class MessageRequestModel : MessageRequest
    {
        [Key]
        public int Id { get; set; }
    }
}