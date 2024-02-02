using bytez.business.Abstract;
using bytez.business.Dto.Product;
using bytez.business.ViewModels.StockVM;
using bytez.entity.Entities;
using bytez.webui.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bytez.webui.Controllers
{
    [Authorize(Roles = "User")]
    public class StockController : Controller
    {
        readonly private IProductService _productService;
        readonly private IProductColorService _productColorService;
        readonly private ICategoryService _categoryService;
        readonly private IBrandModelService _brandModelService;
        readonly private IProductImageService _productImageService;
        public StockController(IProductService productService, IProductColorService productColorService, ICategoryService categoryService, IBrandModelService brandModelService, IProductImageService productImageService)
        {
            _productService = productService;
            _productColorService = productColorService;
            _categoryService = categoryService;
            _brandModelService = brandModelService;
            _productImageService = productImageService;
        }

        public async Task<IActionResult> Index(StockIndexVM model)
        {
            var product = await _productService.GetWhereProduct(model);
            var category = await _categoryService.GetCategoryAsync();
            var brandModel = await _brandModelService.GetBrandsAsync();
            var color = await _productColorService.GetProductColorsAsync();

            StockIndexVM stockIndex = new()
            {
                Products = product,
                Category = category,
                BrandModel = brandModel,
                Color = color

            };


            return View(stockIndex);
        }

        public async Task<IActionResult> Details(string id)
        {
            var product = await _productService.GetByIdAsync(id);

            if (product == null)
            {
                return BadRequest();
            }
            var image = await _productImageService.GetProductImages();
           var data = image.Where(p => p.ProductsId == product.Id);
            StockDetailVM vm = new StockDetailVM() { Product = product, ProductImages = data.ToList() };

            return View(vm);
        }

        
    }
}
