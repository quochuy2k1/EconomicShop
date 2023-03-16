using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web;
using EconomicShop.ViewModel.Abstract;

namespace EconomicShop.ViewModel.Common.Product
{
    public class ProductUpdateRequest : Switchable
    {
        public int? Id { get; set; }
        public string? Name { get; set; } 

        public int? CategoryId { get; set; }

        public HttpPostedFileBase? Image { get; set; } 

        //public string? MoreImages { get; set; } 

        public decimal? Price { get; set; }
        public decimal? OriginalPrice { get; set; }
        public decimal? Promotion { get; set; }
        public int? Warranty { get; set; }

        //[AllowHtml]
        //public string? Description { get; set; }

        public string? Content { get; set; } 
        public bool? HomeFlag { get; set; }
        public bool? HotFlag { get; set; }

        //public string? Tags { set; get; }

        public int? Quantity { set; get; }
    }
}
