using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EconomicShop.Model.Models
{
    [Table("Menus")]
    public class Menu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string URL { get; set; } = null!;

        public int? DisplayOrder { get; set; }

        [Required]
        public int GroupId { get; set; }

        public string Target { get; set; } = null!;

        [Required]
        public bool Status { get; set; }

        [ForeignKey("GroupId")]
        public virtual MenuGroup MenuGroup { get; set; } = null!;
    }
}