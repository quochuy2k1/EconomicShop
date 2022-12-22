using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EconomicShop.Model.Models
{
    [Table("Slides")]
    public class Slide
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [Required]
        [MaxLength(256)]
        public string Name { set; get; } = null!;

        [MaxLength(256)]
        public string Description { set; get; } = null!;

        [MaxLength(256)]
        public string Image { set; get; } = null!;

        [MaxLength(256)]
        public string Url { set; get; } = null!;

        public int? DisplayOrder { set; get; }

        public bool Status { set; get; }

        public string Content { set; get; } = null!;
    }
}