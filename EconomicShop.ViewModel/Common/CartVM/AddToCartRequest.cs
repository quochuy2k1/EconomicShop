using System;

namespace EconomicShop.ViewModel.Common.CartVM
{
    public class AddToCartRequest
    {

        public int ProductId { set; get; }

        public short Quantity { set; get; }

        public decimal Price { set; get; }

    }
}