using EconomicShop.Service.Catalog.Carts;
using EconomicShop.Utilities.Error;
using EconomicShop.ViewModel.Common.CartVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EconomicShop.Controllers
{
    public class CartController : Controller
    {
        private readonly CartService _cartService;

        public CartController()
        {
            _cartService = new CartService();
        }
        // GET: Cart

        [HttpGet]
        [AllowAnonymous]
        public ActionResult _CartHeader()
        {
            ViewBag.ProductTotal = _cartService.GetTotalProductCart();
            return PartialView();
        }


        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Index()
        {

            try
            {
                var userId = Session["UserId"]?.ToString();

                var carts = await _cartService.GetAllCart(userId);

                if (carts == null) return Json(ErrorHelper.JsonError("Không tìm thấy giỏ hàng", JsonStatus.ERROR), JsonRequestBehavior.AllowGet);

                return View(carts);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(ErrorHelper.JsonError(ex.Message, JsonStatus.ERROR), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> AddToCart(AddToCartRequest request)
        {
            if (!ModelState.IsValid) return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ModelState.ToString());

            try
            {
                var cart = await _cartService.AddToCart(request);
                 if(cart == null)
                {
                    return Json(ErrorHelper.JsonError("Không tìm thấy giỏ hàng", JsonStatus.ERROR), JsonRequestBehavior.AllowGet);
                }


                return Json(ErrorHelper.JsonError("Thêm sản phẩm vào giỏ hàng thành công.", JsonStatus.SUCCESS), JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(ErrorHelper.JsonError(ex.Message, JsonStatus.ERROR), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> DeleteCart(int Id)
        {
            if (!ModelState.IsValid) return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ModelState.ToString());

            try
            {
                var cart = await _cartService.DeleteCart(Id);
                if (cart == null)
                {
                    return Json(ErrorHelper.JsonError("Không tìm thấy giỏ hàng", JsonStatus.ERROR), JsonRequestBehavior.AllowGet);
                }


                return Json(ErrorHelper.JsonError("Xoá sản phẩm trong giỏ hàng thành công.", JsonStatus.SUCCESS), JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(ErrorHelper.JsonError(ex.Message, JsonStatus.ERROR), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult UpdateQuantity(int Id, short quantity)
        {
            if (!ModelState.IsValid) return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ModelState.ToString());

            try
            {
                var cart = _cartService.UpdateQuantity(Id, quantity);

                return RedirectToAction("Index");
                //return Json(ErrorHelper.JsonError("Sửa số lượng sản phẩm trong giỏ hàng thành công.", JsonStatus.SUCCESS), JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                //return Json(ErrorHelper.JsonError(ex.Message, JsonStatus.ERROR), JsonRequestBehavior.AllowGet);
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}