using EconomicShop.Model.Abstract;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EconomicShop.Model.Models
{
    [Table("Products")]
    public class Product : Auditable
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = null!;

        public int CategoryId { get; set; }

        [Required]
        [MaxLength(255)]
        public string Image { get; set; } = null!;

        [Column(TypeName = "xml")]
        public string? MoreImages { get; set; }

        public decimal Price { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal? Promotion { get; set; }
        public int? Warranty { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string Description { get; set; } = null!;

        public string Content { get; set; } = null!;
        public bool? HomeFlag { get; set; }
        public bool? HotFlag { get; set; }
        public int? ViewCount { get; set; }

        public string Tags { set; get; }

        public int Quantity { set; get; }

        [ForeignKey("CategoryId")]
        public virtual ProductCategory Caterory { get; set; } = null!;

        public virtual IEnumerable<ProductTag> ProductTags { set; get; }
        public virtual IEnumerable<Cart> Carts { set; get; }
    }
}