using System.Web.Mvc;

namespace EconomicShop.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Admin/Dashboard
        public ActionResult Index()
        {
            ViewBag.DashboardActive = "active";
            return View();
        }
    }
}