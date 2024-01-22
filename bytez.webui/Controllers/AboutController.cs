using bytez.business.Abstract;
using bytez.entity.Entities;
using Microsoft.AspNetCore.Mvc;

namespace bytez.webui.Controllers
{
    public class AboutController : Controller
    {
        readonly private IAboutService _aboutService;

        public AboutController(IAboutService aboutService)
        {
            _aboutService= aboutService;
        }
        public async Task<IActionResult> Index()
        {
            List<About> about =await _aboutService.GetAboutAsync();
            return View(about);
        }
    }
}
