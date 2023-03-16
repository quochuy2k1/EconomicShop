namespace EconomicShop.ViewModel.Common.Product
{
    public class ProductDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public string Image { get; set; } = null!;

        public string? MoreImages { get; set; }

        public decimal Price { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal? Promotion { get; set; }
        public int? Warranty { get; set; }

        public string Description { get; set; } = null!;

        public string Content { get; set; } = null!;
        public int? ViewCount { get; set; }

        public string? Tags { set; get; }

        public int Quantity { set; get; }
    }
}