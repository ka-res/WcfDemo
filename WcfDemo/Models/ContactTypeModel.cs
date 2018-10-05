using System.ComponentModel.DataAnnotations;

namespace WcfDemo
{
    public class ContactTypeModel : IEnumDbModel
    {
        [Key]
        public int Id { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
    }
}