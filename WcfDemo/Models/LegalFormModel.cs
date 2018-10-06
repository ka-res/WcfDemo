using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WcfDemo
{
    [Table("LegalForms")]
    public class LegalFormModel : BaseModel, IEnumDbModel
    {
        [Key]
        public int Id { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
    }
}