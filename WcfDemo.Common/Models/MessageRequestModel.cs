using System.ComponentModel.DataAnnotations;
using WcfDemo.Contracts;

namespace WcfServiceDemo.DataModels
{
    public class MessageRequestModel : MessageRequest
    {
        [Key]
        public int Id { get; set; }
    }
}