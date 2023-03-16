using System;

namespace EconomicShop.ViewModel.Common.CartVM
{
    public class CartViewModel
    {
        public int Id { get; set; }
        public int ProductId { set; get; }
        public string ProductName { set; get; }
        public string ProductImage { set; get; }
        public string CategoryName { set; get; }

        public short Quantity { set; get; }

        public decimal Price { set; get; }

        public DateTime DateCreated { set; get; }

        public string UserId { get; set; }
    }
}