using bytez.business.Abstract;
using bytez.business.Dto.Product;
using bytez.webui.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace bytez.webui.Controllers
{
    public class StockController : Controller
    {
        readonly private IProductService _productService;
        readonly private IProductColorService _productColorService;
        readonly private ICategoryService _categoryService;
        readonly private IBrandModelService _brandModelService;
        public StockController(IProductService productService, IProductColorService productColorService, ICategoryService categoryService, IBrandModelService brandModelService)
        {
            _productService = productService;
            _productColorService = productColorService;
            _categoryService = categoryService;
            _brandModelService = brandModelService;

        }

        public async Task<IActionResult> Index(ProductWhereDto model)
        {
            var product = await _productService.GetWhereProduct(model);
            var category = await _categoryService.GetCategoryAsync();
            var brandModel = await _brandModelService.GetBrandsAsync();
            var color = await _productColorService.GetProductColorsAsync();

            StockIndexVM stockIndex = new() {
                Products = product,
                Category=category,
                BrandModel=brandModel,
                Color= color
            
            };


            return View(stockIndex);
        }

    }
}
