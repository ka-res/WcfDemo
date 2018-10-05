using System.ComponentModel.DataAnnotations;

namespace WcfDemo
{
    public class MessageResponseModel : MessageResponse
    {
        [Key]
        public int Id { get; set; }
    }
}