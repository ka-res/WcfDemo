using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WcfDemo
{
    [Table("ContactTypes")]
    public class ContactTypeModel : BaseModel, IEnumDbModel
    {
        [Key]
        public int Id { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
    }
}