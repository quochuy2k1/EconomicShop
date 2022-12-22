using EconomicShop.Model.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EconomicShop.Model.Models
{
    [Table("Pages")]
    public class Page : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [Required]
        [MaxLength(256)]
        public string Name { set; get; } = null!;

        public string Content { set; get; } = null!;
    }
}