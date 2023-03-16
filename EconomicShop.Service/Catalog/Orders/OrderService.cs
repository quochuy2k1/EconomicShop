using Booking_Movie.ViewModel.Catalog.MomoPaymentVM;
using EconomicShop.Data;
using EconomicShop.Model.Models;
using EconomicShop.Payment.MoMo;
using EconomicShop.Service.Catalog.Carts;
using EconomicShop.Service.Catalog.OrderDetails;
using EconomicShop.Service.Catalog.ProductCategories;
using EconomicShop.Service.Catalog.Products;
using EconomicShop.Utilities.Ngrok;
using EconomicShop.ViewModel.Common.MomoPaymentVM;
using EconomicShop.ViewModel.Common.OrderVM;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;
using static System.Collections.Specialized.BitVector32;

namespace EconomicShop.Service.Catalog.Orders
{
    public class OrderService
    {
        private readonly QHuyShopDbContext _dbContext;
        private readonly OrderDetailService _orderDetailService;
        private readonly ProductService _productService;
        private readonly CartService _cartService;

        public OrderService()
        {
            _dbContext = new QHuyShopDbContext();
            _orderDetailService = new OrderDetailService();
            _productService = new ProductService();
            _cartService = new CartService();
        }

        public string MomoPayment(CreateMomoPaymentRequest request)
        {
            //request params need to request to MoMo system
            string endpoint = ConfigurationManager.AppSettings["MomoEndpoint"]!;
            string partnerCode = ConfigurationManager.AppSettings["PartnerCode"]!;
            string partnerName = "Berry Shop";
            string accessKey = ConfigurationManager.AppSettings["AccessKey"]!;
            string serectkey = ConfigurationManager.AppSettings["Serectkey"]!;
            string orderInfo = request.OrderInfo;
            string redirectUrl = ConfigurationManager.AppSettings["RedirectUrl"]!;
            var ngrok = NgrokHelper.CreateEmbeddataWithPublicUrl();
            string ipnUrl = ngrok["callbackurl"].ToString(); //lưu ý: notifyurl không được sử dụng localhost, có thể sử dụng ngrok để public localhost trong quá trình test

            string amount = request.Amount.ToString();
            string orderId = DateTime.Now.Ticks.ToString(); //mã đơn hàng
            string requestId = DateTime.Now.Ticks.ToString();
            string requestType = "captureWallet";

            //

            var userId = HttpContext.Current.Session["UserId"]?.ToString();

            request.ExtraData.Order.UserId = userId;
            request.ExtraData.Order.OrderId = orderId;
            string extraData = Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(request.ExtraData!)));
            string lang = "vi";

            //Before sign HMAC SHA256 signature
            string rawHash = $"accessKey={accessKey}&amount={amount}&extraData={extraData}&ipnUrl={ipnUrl}" +
                $"&orderId={orderId}&orderInfo={orderInfo}&partnerCode={partnerCode}&redirectUrl={redirectUrl}" +
                $"&requestId={requestId}&requestType={requestType}";

            //sign signature SHA256
            MoMoSecurity moMoSecurity = new MoMoSecurity();
            string signature = moMoSecurity.signSHA256(rawHash, serectkey);

            //build body json request
            JObject message = new JObject
            {
                { "partnerCode", partnerCode },
                { "partnerName", partnerName },
                { "requestId", requestId },
                { "amount", amount },
                { "orderId", orderId },
                { "orderInfo", orderInfo },
                { "redirectUrl", redirectUrl },
                { "ipnUrl", ipnUrl },
                { "extraData", extraData },
                { "requestType", requestType },
                { "signature", signature },
                { "lang", lang }

            };
            string responseFromMomo = PaymentRequest.sendPaymentRequest(endpoint, JsonConvert.SerializeObject(message));

            JObject jmessage = JObject.Parse(responseFromMomo);
            return jmessage.GetValue("payUrl")!.ToString();
        }


        public async Task<int?> SavePayment(ResultMomoPaymentViewModel result)
        {
            var extraData = Encoding.UTF8.GetString(Convert.FromBase64String(result.ExtraData));
            var orderCreateRequest = JObject.Parse(extraData).ToObject<ExtraData>();

            var Order = orderCreateRequest.Order;
            var OrderDetail = orderCreateRequest.OrderDetail;
            var order = new Order()
            {
                CustomerName = Order.CustomerName,
                CustomerAddress = Order.CustomerAddress,
                CustomerEmail = Order.CustomerEmail,
                CustomerMessage = Order.CustomerMessage,
                CustomerMobile = Order.CustomerMobile,
                OrderId = Order.OrderId,
                PaymentMethod = Order.PaymentMethod,
                Total = Order.Total,
                UserId = Order.UserId,
                PaymentStatus = "false",
                Status = true,
                CreatedDate = DateTime.Now

            };

            var addOrder = this.SaveMomoPayment(order);

            if (await _dbContext.SaveChangesAsync() > 0)
            {
                var addOrderDetails = await _orderDetailService.AddOrderDetails(addOrder.ID, OrderDetail);


                if (addOrderDetails != null)
                {
                    foreach (var orderDetail in addOrderDetails)
                    {
                        await _productService.UpdateQuantity(Convert.ToInt32(orderDetail["ProductId"]), Convert.ToInt32(orderDetail["Quantity"]), Enums.UpdateQuantityAction.Decrement);
                        await _cartService.DeleteCart(Convert.ToInt32(orderDetail["CartId"]), Order.UserId);
                    }
                }

                await _dbContext.SaveChangesAsync();
            };


            return order.ID;
        }

        public Order SaveMomoPayment(Order order)
        {
            var Entity = _dbContext.Orders.Add(order);
            return Entity;
        }

        public async Task<bool> UpdatePaymentStatus(int Id, string status)
        {
            var order = await _dbContext.Orders.FindAsync(Id);

            if (order == null) return false;

            order.PaymentStatus = status;

            _dbContext.Entry(order).Property(x => x.PaymentStatus).IsModified = true;
            return true;
        }
    }
}
