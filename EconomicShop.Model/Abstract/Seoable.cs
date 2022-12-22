using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EconomicShop.Model.Abstract
{
    public class Seoable : Switchable, ISeoable
    {
#nullable enable

        [Required]
        [Column(TypeName = "varchar")]
        [MaxLength(256)]
        public string Alias { get; set; } = null!;

        [MaxLength(255)]
        public string? MetaKeyword { get; set; }

        [MaxLength(255)]
        public string? MetaDescription { get; set; }
    }
}