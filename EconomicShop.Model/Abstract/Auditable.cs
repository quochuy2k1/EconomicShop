using System;
using System.ComponentModel.DataAnnotations;

namespace EconomicShop.Model.Abstract
{
    /// <summary>
    /// Kế thừa nhiều quá 🤣🤣🤣
    /// Có thể gôm thành 1 Auditable class
    /// </summary>
    public abstract class Auditable : Seoable, IAuditable
    {
#nullable enable
        public DateTime CreatedDate { get; set; }

        [MaxLength(255)]
        public string? CreatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        [MaxLength(255)]
        public string? UpdatedBy { get; set; }
    }
}