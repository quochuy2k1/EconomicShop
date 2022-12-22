using EconomicShop.Model.Abstract;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EconomicShop.Model.Models
{
    [Table("PostCategories")]
    public class PostCategory : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [Required]
        [MaxLength(256)]
        public string Name { set; get; } = null!;

        [MaxLength(500)]
        public string Description { set; get; } = null!;

        public int? ParentID { set; get; }
        public int? DisplayOrder { set; get; }

        [MaxLength(256)]
        public string Image { set; get; } = null!;

        public bool? HomeFlag { set; get; }

        public virtual IEnumerable<Post> Posts { set; get; } = null!;
    }
}