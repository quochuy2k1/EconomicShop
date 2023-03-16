using EconomicShop.ViewModel.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EconomicShop.ViewModel.Common.Product
{
    public class ProductPagingRequest: PagingRequestBase
    {
        public int? CategoryId { get; set; }
        public string? Keyword { get; set; }
    }
}
