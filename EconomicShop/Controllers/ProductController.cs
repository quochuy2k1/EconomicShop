using EconomicShop.Service.Catalog.Products;
using EconomicShop.Utilities.Error;
using EconomicShop.ViewModel.Common.Product;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EconomicShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductService _productService;

        public ProductController()
        {
            _productService = new ProductService();
        }
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> _GetAllPagingProduct(ProductPagingRequest request)
        {
            if (!ModelState.IsValid) return Json(ErrorHelper.JsonError(ModelState, JsonStatus.ERROR), JsonRequestBehavior.AllowGet);

            try
            {
                var products = await _productService.GetAllPagingProduct(request);

                if (products == null) return Json(ErrorHelper.JsonError("Sản phẩm trống", JsonStatus.ERROR), JsonRequestBehavior.AllowGet);

                return PartialView(products);
            }
            catch (System.Exception ex)
            {

                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(ErrorHelper.JsonError(ex.Message, JsonStatus.ERROR), JsonRequestBehavior.AllowGet);
            }
            
        }

        [HttpGet]
        public async Task<ActionResult> ProductDetail(int Id)
        {
            if (!ModelState.IsValid) return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Không có Id như vậy");
            var product = await _productService.GetProductDetail(Id);

            try
            {
                if(product == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Không tìm thấy sản phẩm");
                }
                return View(product);
            }
            catch (System.Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}