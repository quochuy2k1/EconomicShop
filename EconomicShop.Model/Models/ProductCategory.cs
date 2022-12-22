using EconomicShop.Model.Abstract;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EconomicShop.Model.Models
{
    [Table("ProductCategories")]
    public class ProductCategory : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(512)]
        public string Description { get; set; } = null!;

        public int? ParentId { get; set; }
        public int? DisplayOrder { get; set; }

        [Required]
        [MaxLength(255)]
        public string Image { get; set; } = null!;

        public bool HomeFlag { get; set; }

        public virtual IEnumerable<Product> Products { get; set; } = null!;
    }
}