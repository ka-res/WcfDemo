using System.ComponentModel.DataAnnotations;
using WcfDemo;

namespace WcfServiceDemo.DataModels
{
    public class MessageRequestModel : MessageRequest
    {
        [Key]
        public int Id { get; set; }
    }
}