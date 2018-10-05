using System.ComponentModel.DataAnnotations;
using WcfDemo;

namespace WcfServiceDemo.DataModels
{
    public class MessageResponseModel : MessageResponse
    {
        [Key]
        public int Id { get; set; }
    }
}