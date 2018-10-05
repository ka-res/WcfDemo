using System.ComponentModel.DataAnnotations;

namespace WcfDemo
{
    public class ContactModel : Contact
    {
        [Key]
        public int Id { get; set; }
    }
}