using System.ComponentModel.DataAnnotations;

namespace WcfDemo
{
    public class LegalFormModel : IEnumDbModel
    {
        [Key]
        public int Id { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
    }
}