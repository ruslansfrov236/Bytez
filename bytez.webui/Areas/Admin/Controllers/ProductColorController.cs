using bytez.business.Abstract;
using bytez.business.Dto.ProducColor;
using bytez.entity.Entities;
using Microsoft.AspNetCore.Mvc;

namespace bytez.webui.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class ProductColorController : Controller
    {
        readonly private IProductColorService _productColorService;
      

        public ProductColorController(IProductColorService productColorService  )
        {
            _productColorService= productColorService;
               
        }
        public async Task<IActionResult> Index()
        {
            List<ProductColor> productColors= await _productColorService.GetProductColorsAsync();
            return View(productColors);
        }
        public async Task<IActionResult> Details(string id)
        {
            var color = await _productColorService.GetProductColorByIdAsync(id);
            if (color == null) return NotFound();
            return View(color);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        => View();
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductColorDto model)
        {
         
             if(!ModelState.IsValid) return View();
            await _productColorService.Create( model);

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            var color = await _productColorService.GetProductColorByIdAsync(id);
            if (color == null) return NotFound();

            UpdateProductColorDto updateProductColorDto = new() { ColorName = color.ColorName };
            return View(updateProductColorDto);
        }

        [HttpPost]
        public async Task<IActionResult> Update(string id, UpdateProductColorDto model)
        {
            if (id != model.id) return BadRequest();

            await _productColorService.Update(model);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var color = await _productColorService.GetProductColorByIdAsync(id);
            if (color == null) return NotFound();

            await _productColorService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
