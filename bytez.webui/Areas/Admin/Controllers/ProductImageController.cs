using bytez.business.Abstract;
using bytez.business.Dto.ProductImage;
using Microsoft.AspNetCore.Mvc;

namespace bytez.webui.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class ProductImageController : Controller
    {
        readonly private IProductImageService _productImageService;

        public ProductImageController(IProductImageService productImageService)
        {
            _productImageService= productImageService;
        }
        public async Task<IActionResult> Index ()
        {
            var image = await _productImageService.GetProductImages();
            
            return View(image);
        }
        public async Task<IActionResult> ShowCase()
        => View();
        //public async Task<IActionResult> ShowCase(ShowCaseDto model)
        //{
        //    var image = await _productImageService.GetProductImagesById(model.id);
        //    if (image == null) return NotFound();
        //    await _productImageService.ShowCaseImage(model.id, model.showCase);

        //    return RedirectToAction(nameof(Index), "Product");
        //}
    }
}
