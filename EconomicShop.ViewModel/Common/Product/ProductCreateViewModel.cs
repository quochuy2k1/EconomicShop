using EconomicShop.ViewModel.Abstract;
using System.Web;
using System.Web.Mvc;

namespace EconomicShop.ViewModel.Common.Product
{
    public class ProductCreateViewModel : Seoable
    {
        public string Name { get; set; } = null!;

        public int CategoryId { get; set; }

        public HttpPostedFileBase Image { get; set; } = null!;

        public string MoreImages { get; set; } = null!;

        public decimal Price { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal? Promotion { get; set; }
        public int? Warranty { get; set; }

        [AllowHtml]
        public string Description { get; set; } = null!;

        public string Content { get; set; } = null!;
        public bool? HomeFlag { get; set; }
        public bool? HotFlag { get; set; }

        public string Tags { set; get; }

        public int Quantity { set; get; }
    }
}