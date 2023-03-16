using EconomicShop.ViewModel.Abstract;
using System.Web;
using System.Web.Mvc;

namespace EconomicShop.ViewModel.Common.ProductCategory
{
    public class ProductCategoryCreateRequest : Switchable
    {
        public string Name { get; set; } = null!;

        [AllowHtml]
        public string Description { get; set; } = null!;

        public int? ParentId { get; set; }
        public int? DisplayOrder { get; set; }

        public HttpPostedFileBase Image { get; set; } = null!;

        public bool HomeFlag { get; set; }
    }
}