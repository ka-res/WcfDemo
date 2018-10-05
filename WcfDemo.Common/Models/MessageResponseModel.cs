using System.ComponentModel.DataAnnotations;
using WcfDemo.Contracts;

namespace WcfServiceDemo.DataModels
{
    public class MessageResponseModel : MessageResponse
    {
        [Key]
        public int Id { get; set; }
    }
}