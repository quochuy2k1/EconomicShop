namespace EconomicShop.ViewModel.Abstract
{
    public class Seoable : Switchable, ISeoable
    {

        public string? Alias { get; set; }

        public string? MetaKeyword { get; set; }

        public string? MetaDescription { get; set; }
    }
}