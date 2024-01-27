using bytez.business.Abstract;
using bytez.business.Dto.Blog;
using bytez.entity.Entities;
using Microsoft.AspNetCore.Mvc;

namespace bytez.webui.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class BlogController : Controller
    {
        readonly private IBlogService _blogService;
        public async Task<IActionResult> Index()
        {
            List<Blog> blogs = await _blogService.GetBlogListAsync();

            return View(blogs);
        }
        public async Task<IActionResult> Details(string id)
        {
            var blog = await _blogService.GetBlogById(id);
            if (blog == null) return NotFound();

            return View(blog);
        }
        public async Task<IActionResult> Create()
            => View();
        [HttpPost]
        public async Task<IActionResult> Create(CreateBlogDto model)
        {
            if (!ModelState.IsValid) return View(model);
            await _blogService.Create(model);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(string id)
        {
            var blog = await _blogService.GetBlogById(id);
            if (blog == null) return NotFound();
            UpdateBlogDto updateBlogDto = new UpdateBlogDto()
            {
                Title=blog.Title,
                Description=blog.Description,
                ContentInformation=blog.ContentInformation,
                FilePath=blog.FilePath
            };

            return View(updateBlogDto);

        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateBlogDto model )
        {
            if (!ModelState.IsValid) return View(model);
            var blog = await _blogService.GetBlogById(model.id);
            if (blog == null) return BadRequest();

            await _blogService.Update(model);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var blog = await _blogService.GetBlogById(id);
            if (blog == null) return BadRequest();
            return RedirectToAction(nameof(Index));
        }
    }
}
