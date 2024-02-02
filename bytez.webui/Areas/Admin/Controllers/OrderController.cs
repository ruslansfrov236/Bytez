using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bytez.webui.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        [Authorize(Roles = "Admin , Manager")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
