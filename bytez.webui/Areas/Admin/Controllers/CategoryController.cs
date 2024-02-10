using bytez.business.Abstract;
using bytez.business.Dto.Category;
using bytez.entity.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bytez.webui.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class CategoryController : Controller
    {
        readonly private ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [Authorize(Roles = "Admin , Manager")]
        public async Task<IActionResult> Index()
        {

            List<Category> categories = await _categoryService.GetCategoryAsync();

            return View(categories);
        }
        [Authorize(Roles = "Admin , Manager")]
        public async Task<IActionResult> Details(string id)
        {
            var categories = await _categoryService.GetCategoryByIdAsync(id);
            if (categories == null) return NotFound();
            return View(categories);
        }
        [Authorize(Roles = "Admin , Manager")]
        public async Task<IActionResult> Create()
        => View(new CreateCategoryDto());
        [HttpPost]
        [Authorize(Roles = "Admin ")]
        public async Task<IActionResult> Create(CreateCategoryDto model)
        {
            if (!ModelState.IsValid) return View(model);

            var category = await _categoryService.GetCategoryAsync();
            bool isReplayValue = category.Any(c => c.Name.ToLower().Trim() == model.Name.ToLower().Trim());

            if (isReplayValue)
            {
                ModelState.AddModelError("Name", "Tekrarlana alan ");
            }
            await _categoryService.Create(model);

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        [Authorize(Roles = "Admin ")]
        public async Task<IActionResult> Update(string id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            UpdateCategoryDto updateCategoryDto = new() { Name=category.Name };

            if (updateCategoryDto == null) return NotFound();
            return View(updateCategoryDto);
        }
        [HttpPost]
        [Authorize(Roles = "Admin ")]
        public async Task<IActionResult> Update(UpdateCategoryDto model)
        {
            if (!ModelState.IsValid) return View(model);
            await _categoryService.Update(model);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [Authorize(Roles = "Admin ")]
        public async Task<IActionResult> Delete(string id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null) return NotFound();

            await _categoryService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
