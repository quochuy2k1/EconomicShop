using EconomicShop.ViewModel.Abstract;

namespace EconomicShop.ViewModel.Common.ProductCategory
{
    public class ProductCategoryViewModel : Switchable
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public int? ParentId { get; set; }
        public int? DisplayOrder { get; set; }

        public string Image { get; set; } = null!;

        public bool HomeFlag { get; set; }
    }
}