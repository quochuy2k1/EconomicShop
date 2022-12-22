using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EconomicShop.Model.Models
{
    [Table("ProductTags")]
    public class ProductTag
    {
        [Key]
        [Column(Order = 1)]
        public int ProductID { set; get; }

        [Key]
        [Column(TypeName = "varchar", Order = 2)]
        [MaxLength(50)]
        public string TagID { set; get; } = null!;

        [ForeignKey("ProductID")]
        public virtual Product Product { set; get; } = null!;

        [ForeignKey("TagID")]
        public virtual Tag Tag { set; get; } = null!;
    }
}