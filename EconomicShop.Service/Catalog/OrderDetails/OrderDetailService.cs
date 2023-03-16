using EconomicShop.Data;
using EconomicShop.Model.Models;
using EconomicShop.ViewModel.Common.OrderDetailVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EconomicShop.Service.Catalog.OrderDetails
{

    public class OrderDetailService
    {
        private readonly QHuyShopDbContext _dbContext;

        public OrderDetailService()
        {
            _dbContext = new QHuyShopDbContext();
        }


        public async Task<List<Dictionary<string, object>>> AddOrderDetails(int OrderId, List<CreateOrderDetailRequest> requests)
        {
            List<Dictionary<string, object>> orderDetails = new List<Dictionary<string, object>>();
            if (requests.Count == 1)
            {
                var orderDetail = requests[0];
                var Entity = new OrderDetail() { 
                    OrderID = OrderId,
                    Price = orderDetail.Price,
                    ProductID = orderDetail.ProductID,
                    Quantity = orderDetail.Quantity
                };

                _dbContext.OrderDetails.Add(Entity);
                orderDetails.Add(new Dictionary<string, object>
                {
                    {"ProductId", Entity.ProductID },
                    {"Quantity", Entity.Quantity },
                    {"CartId", orderDetail.Id }
                });
                await _dbContext.SaveChangesAsync();


            }
            else if(requests.Count > 1)
            {
                foreach (var entity in requests)
                {
                    
                    var Entity = new OrderDetail()
                    {
                        OrderID = OrderId,
                        Price = entity.Price,
                        ProductID = entity.ProductID,
                        Quantity = entity.Quantity
                    };

                    _dbContext.OrderDetails.Add(Entity);
                    orderDetails.Add(new Dictionary<string, object>
                {
                    {"ProductId", Entity.ProductID },
                    {"Quantity", Entity.Quantity },
                    {"CartId", entity.Id }
                });
                    await _dbContext.SaveChangesAsync();

                }
            }

            return orderDetails;

        }
    }
}
