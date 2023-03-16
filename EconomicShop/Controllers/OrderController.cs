using Booking_Movie.ViewModel.Catalog.MomoPaymentVM;
using EconomicShop.Service.Catalog.Orders;
using EconomicShop.Utilities.Error;
using EconomicShop.ViewModel.Common.MomoPaymentVM;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EconomicShop.Controllers
{
    public class OrderController : Controller
    {
        private readonly OrderService _orderService;

        public OrderController()
        {
            _orderService = new OrderService();
        }
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult MomoPayment(CreateMomoPaymentRequest request)
        {
            try
            {
                var momo =  _orderService.MomoPayment(request);
                if (momo == null) return Json(ErrorHelper.JsonError("Thanh toán không thành công.", JsonStatus.SUCCESS), JsonRequestBehavior.AllowGet); ;

                var result = new Dictionary<string, object>
                {
                    {"message" , "Thanh toán sản phẩm trong giỏ hàng thành công." },
                    {"order_url" , momo }
                };
                
                return Json(ErrorHelper.JsonError(result, JsonStatus.SUCCESS), JsonRequestBehavior.AllowGet); ;

            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(ErrorHelper.JsonError(ex.Message, JsonStatus.ERROR), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonResult> SavePayment(ResultMomoPaymentViewModel request)
        {
            //cập nhật dữ liệu vào db
            try
            {
                //var restult = JObject.Parse(request.ToString()!).ToObject<ResultMomoPaymentViewModel>();
                var booking = await _orderService.SavePayment(request!);
                return Json(ErrorHelper.JsonError("Thanh toán thành công.", JsonStatus.SUCCESS), JsonRequestBehavior.AllowGet); ;
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(ErrorHelper.JsonError(ex.Message, JsonStatus.ERROR), JsonRequestBehavior.AllowGet);
            }

        }
    }
}