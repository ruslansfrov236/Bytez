using bytez.business.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bytez.webui.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class OrderController : Controller
    {
        readonly private IOrderService  _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService=orderService;
        }
        [Authorize(Roles = "Admin , Manager")]
        public async Task<IActionResult> Index()
        {
            var order = await _orderService.GetOrderAsync();
            return View(order);
        }
    }
}
