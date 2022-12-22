using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EconomicShop.Model.Models
{
    [Table("Footers")]
    public class Footer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ID { get; set; } = null!;

        [Required]
        public string Content { get; set; } = null!;
    }
}