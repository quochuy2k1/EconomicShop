using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EconomicShop.Model.Models
{
    public class Cart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }
        [Required]
        public int ProductId { set; get; }

        [Required]
        public short Quantity { set; get; }
        [Required]

        public decimal Price { set; get; }
        [Required]
        public DateTime DateCreated { set; get; }

        [Required]
        [StringLength(128)]
        [Column(TypeName = "nvarchar")]
        public string UserId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { set; get; }
        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { set; get; }

    }
}
