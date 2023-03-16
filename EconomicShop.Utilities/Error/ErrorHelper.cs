using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EconomicShop.Utilities.Error
{
    public static class ErrorHelper
    {
        public static Dictionary<string, object> JsonError(object result, string status)
        {
            return new Dictionary<string, object> {
                        {"result", result },
                         {"status", status }
                    };
        }
    }
}
