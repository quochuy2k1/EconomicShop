namespace EconomicShop.ViewModel.Common.OrderDetailVM
{
    public class CreateOrderDetailRequest
    {
        public int Id { get; set; }
        public int ProductID { set; get; }

        public int Quantity { set; get; }

        public decimal Price { set; get; }
    }
}