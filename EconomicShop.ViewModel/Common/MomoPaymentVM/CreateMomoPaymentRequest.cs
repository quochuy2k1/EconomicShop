using EconomicShop.ViewModel.Common.OrderDetailVM;
using EconomicShop.ViewModel.Common.OrderVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EconomicShop.ViewModel.Common.MomoPaymentVM
{
    public class CreateMomoPaymentRequest
    {
        public string? OrderInfo { get; set; }
        public decimal Amount { get; set; }
        public ExtraData? ExtraData { get; set; }
    }

    public class ExtraData
    {
        public CreateOrderRequest Order { get; set; }
        public List<CreateOrderDetailRequest> OrderDetail { get; set; }
    }
}
