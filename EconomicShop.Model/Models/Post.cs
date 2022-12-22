
using EconomicShop.Model.Abstract;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EconomicShop.Model.Models
{
    [Table("Posts")]
    public class Post : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [Required]
        [MaxLength(256)]
        public string Name { set; get; } = null!;

        [Required]
        public int CategoryID { set; get; }

        [MaxLength(256)]
        public string Image { set; get; } = null!;

        [MaxLength(500)]
        public string Description { set; get; } = null!;

        public string Content { set; get; } = null!;

        public bool? HomeFlag { set; get; }
        public bool? HotFlag { set; get; }
        public int? ViewCount { set; get; }

        [ForeignKey("CategoryID")]
        public virtual PostCategory PostCategory { set; get; } = null!;

        public virtual IEnumerable<PostTag> PostTags { set; get; } = null!;
    }
}