using System;
using System.ComponentModel.DataAnnotations;

namespace EconomicShop.ViewModel.Abstract
{
    /// <summary>
    /// Kế thừa nhiều quá 🤣🤣🤣
    /// Có thể gôm thành 1 Auditable class
    /// </summary>
    public abstract class Auditable : Seoable, IAuditable
    {
        public DateTime CreatedDate { get; set; }

        [MaxLength(255)]
        public string? CreatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        [MaxLength(255)]
        public string? UpdatedBy { get; set; }
    }
}