using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EconomicShop.Model.Models
{
    [Table("SupportOnlines")]
    public class SupportOnline
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [Required]
        [MaxLength(50)]
        public string Name { set; get; } = null!;

        [MaxLength(50)]
        public string Department { set; get; } = null!;

        [MaxLength(50)]
        public string Skype { set; get; } = null!;

        [MaxLength(50)]
        public string Mobile { set; get; } = null!;

        [MaxLength(50)]
        public string Email { set; get; } = null!;

        [MaxLength(50)]
        public string Yahoo { set; get; } = null!;

        [MaxLength(50)]
        public string Facebook { set; get; } = null!;

        public bool Status { set; get; }

        public int? DisplayOrder { set; get; }
    }
}