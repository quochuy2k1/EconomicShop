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

        [MaxLength(50)]
        public string OrderId { set; get; }

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

       
        [MaxLength(256)]
        public string? CustomerMessage { set; get; }

        [MaxLength(256)]
        public string PaymentMethod { set; get; } = null!;

        public decimal Total { get; set; }

        public DateTime? CreatedDate { set; get; }
        public string? CreatedBy { set; get; }
        public string PaymentStatus { set; get; } = null!;
        public bool Status { set; get; }

        [StringLength(128)]
        [Column(TypeName = "nvarchar")]
        public string UserId { set; get; } = null!;

        [ForeignKey("UserId")]
        public virtual IEnumerable<ApplicationUser> ApplicationUsers { set; get; } = null!;
    }
}