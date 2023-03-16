using EconomicShop.Service.Catalog.ProductCategories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EconomicShop.Controllers
{
    public class ProductCategoryController : Controller
    {
        private readonly ProductCategoryService _productCategoryService;

        public ProductCategoryController()
        {
            _productCategoryService = new ProductCategoryService();
        }


        // GET: ProductCategory
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> _ProductCategoryHome()
        {
            if(!ModelState.IsValid) return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ModelState.ToString());
            var productCategories = await _productCategoryService.GetAllProductCategory();
            if(productCategories == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Danh mục sản phẩm trống.");

            return PartialView(productCategories);
        }
    }
}