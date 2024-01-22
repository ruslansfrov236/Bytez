using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Webui.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
