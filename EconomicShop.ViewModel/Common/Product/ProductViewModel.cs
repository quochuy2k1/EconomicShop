using EconomicShop.ViewModel.Abstract;

namespace EconomicShop.ViewModel.Common.Product
{
    public class ProductViewModel : Switchable
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public string CategoryName { get; set; }
        public string? Content { get; set; }
        public int CategoryId { get; set; }

        public string Image { get; set; } = null!;

        public decimal Price { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal? Promotion { get; set; }

        public string Description { get; set; } = null!;

        public bool? HomeFlag { get; set; }
        public bool? HotFlag { get; set; }

        public int Quantity { set; get; }
    }
}