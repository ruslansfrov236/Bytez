using bytez.business.Abstract;
using bytez.business.Dto.Blog;
using bytez.entity.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bytez.webui.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }
        [Authorize(Roles = "Admin , Manager")]
        public async Task<IActionResult> Index()
        {
            List<Blog> blogs = await _blogService.GetBlogListAsync();
            return View(blogs);
        }
        [Authorize(Roles = "Admin , Manager")]
        public async Task<IActionResult> Details(string id)
        {
            var blog = await _blogService.GetBlogById(id);
            if (blog == null) return NotFound();

            return View(blog);

        }
        [Authorize(Roles = "Admin ")]
        public async Task<IActionResult> Create()
        => View(new CreateBlogDto());
        [HttpPost]
        [Authorize(Roles = "Admin ")]
        public async Task<IActionResult> Create(CreateBlogDto model)
        {
            if (!ModelState.IsValid) return View(model);
            await _blogService.Create(model);

            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Admin ")]
        public async Task<IActionResult> Update(string id)
        {
            var blog = await _blogService.GetBlogById(id);
            if (blog == null) return NotFound();

            UpdateBlogDto updateBlogDto = new UpdateBlogDto()
            {

                id=blog.Id.ToString(),
                Title=blog.Title,
                Description=blog.Description,
                ContentInformation=blog.ContentInformation,
                isVideo=blog.isVideo,
                VideoPath=blog.VideoPath,
                FilePath=blog.FilePath


            };

            return View(updateBlogDto);

        }
        [HttpPost]
        [Authorize(Roles = "Admin ")]
        public async Task<IActionResult> Update(UpdateBlogDto model)
        {
            if (!ModelState.IsValid) return View(model);
            await _blogService.Update(model);

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [Authorize(Roles = "Admin ")]
        public async Task<IActionResult>  Delete (string id)
        {
            var blog = await _blogService.GetBlogById(id);
            if (blog == null) return NotFound();

            await _blogService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
