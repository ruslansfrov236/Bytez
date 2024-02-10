using bytez.business.Abstract;
using bytez.entity.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bytez.webui.Controllers
{
    [Authorize(Roles = "User , Manager , Admin ")]
    public class BlogController : Controller
    {
        readonly private IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;  
        }
        public async Task<IActionResult> Index()
        {
            var blog = await _blogService.GetBlogListAsync();

            return View(blog);
        }

        public async Task<IActionResult> Details(string id ) {
            var blog = await _blogService.GetBlogById(id);
            if (blog == null) return NotFound();
            return View(blog);
        }
    }
}
