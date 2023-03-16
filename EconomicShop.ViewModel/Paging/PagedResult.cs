using System.Collections.Generic;

namespace EconomicShop.ViewModel.Paging
{
    public class PagedResult<T>
    {
        public List<T> Items { get; set; }
        public int TotalRecord { get; set; }
    }
}
