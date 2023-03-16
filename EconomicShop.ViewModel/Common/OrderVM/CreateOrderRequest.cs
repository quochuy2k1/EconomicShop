namespace EconomicShop.ViewModel.Common.OrderVM
{
    public class CreateOrderRequest
    {
        public string? OrderId { get; set; }
        public string? UserId { set; get; }
        public string CustomerName { set; get; } = null!;

        public string CustomerAddress { set; get; } = null!;

        public string CustomerEmail { set; get; } = null!;

        public string CustomerMobile { set; get; } = null!;

        public string? CustomerMessage { set; get; }

        public string PaymentMethod { set; get; } = null!;

        public decimal Total { get; set; }
    }
}