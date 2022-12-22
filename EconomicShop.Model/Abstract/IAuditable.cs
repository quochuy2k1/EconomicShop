using System;

namespace EconomicShop.Model.Abstract
{
    public interface IAuditable
    {
#nullable enable
        DateTime CreatedDate { get; set; }
        string? CreatedBy { get; set; }
        DateTime UpdatedDate { get; set; }
        string? UpdatedBy { get; set; }
    }
}