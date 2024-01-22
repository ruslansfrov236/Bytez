using bytez.business.Abstract;
using bytez.business.Dto.BrandModel;
using bytez.entity.Entities;
using Microsoft.AspNetCore.Mvc;

namespace bytez.webui.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class BrandModelController : Controller
    {
        readonly private IBrandModelService _brandModelService;

        public BrandModelController(IBrandModelService brandModelService)
        {
            _brandModelService = brandModelService;
        }
        public async Task<IActionResult> Index()
        {
            List<BrandModel> brandModels = await _brandModelService.GetBrandsAsync();

            return View(brandModels);

        }

        public async Task<IActionResult> Details(string id)
        {
            var brandModels = await _brandModelService.GetBrandByIdAsync(id);
            if (brandModels == null) return NotFound();
            return View(brandModels);
        }
        public async Task<IActionResult> Create()
        => View();
        [HttpPost]
        public async Task<IActionResult> Create(CreateBrandModelDto model)
        {
            var brandsModel = await _brandModelService.GetBrandsAsync();
            bool isReplayValue = brandsModel.Any(br => br.Name.ToLower().Trim() == model.Name.ToLower().Trim());

            if (isReplayValue)
            {
                ModelState.AddModelError("Name", "Tekrarlana alan ");
            }
            await _brandModelService.Create(model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(string id)
        {
            var brandModels = await _brandModelService.GetBrandByIdAsync(id);
            if (brandModels == null) return NotFound();
            UpdateBrandModelDto updateBrandModelDto = new()
            {
                FilePath = brandModels.FilePath,
                Name = brandModels.Name,
                formFile = brandModels.formFile 
            }; return View(updateBrandModelDto);


        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateBrandModelDto model)
        {
            await _brandModelService.Update(model);
            return RedirectToAction(nameof(Index));

        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var brandModels = await _brandModelService.GetBrandByIdAsync(id);
            if (brandModels == null) return NotFound();
            await _brandModelService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
