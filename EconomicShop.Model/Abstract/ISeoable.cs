namespace EconomicShop.Model.Abstract
{
    public interface ISeoable
    {
        string? Alias { get; set; }
        string? MetaKeyword { get; set; }
        string? MetaDescription { get; set; }
    }
}