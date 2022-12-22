using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EconomicShop.Model.Models
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [Required]
        [MaxLength(256)]
        public string CustomerName { set; get; } = null!;

        [Required]
        [MaxLength(256)]
        public string CustomerAddress { set; get; } = null!;

        [Required]
        [MaxLength(256)]
        public string CustomerEmail { set; get; } = null!;

        [Required]
        [MaxLength(50)]
        public string CustomerMobile { set; get; } = null!;

        [Required]
        [MaxLength(256)]
        public string CustomerMessage { set; get; } = null!;

        [MaxLength(256)]
        public string PaymentMethod { set; get; } = null!;

        public DateTime? CreatedDate { set; get; }
        public string CreatedBy { set; get; } = null!;
        public string PaymentStatus { set; get; } = null!;
        public bool Status { set; get; }

        [StringLength(128)]
        [Column(TypeName = "nvarchar")]
        public string CustomerId { set; get; } = null!;

        [ForeignKey("CustomerId")]
        public virtual IEnumerable<OrderDetail> OrderDetails { set; get; } = null!;
    }
}