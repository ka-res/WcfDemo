using System.ComponentModel.DataAnnotations;
using WcfDemo;

namespace WcfServiceDemo.DataModels
{
    public class ContactModel : Contact
    {
        [Key]
        public int Id { get; set; }
    }
}