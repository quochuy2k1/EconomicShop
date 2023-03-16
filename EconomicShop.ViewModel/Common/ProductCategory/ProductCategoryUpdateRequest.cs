using EconomicShop.ViewModel.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EconomicShop.ViewModel.Common.ProductCategory
{
    public class ProductCategoryUpdateRequest : Switchable
    {
        public int Id { get; set; }

        public string? Name { get; set; }
        [AllowHtml]
        public string? Description { get; set; }

        public int? ParentId { get; set; }
        public int? DisplayOrder { get; set; }

        public HttpPostedFileBase? Image { get; set; } 

        public bool? HomeFlag { get; set; }
    }
}
