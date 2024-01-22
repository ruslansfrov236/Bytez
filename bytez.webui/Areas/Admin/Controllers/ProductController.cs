using bytez.business.Abstract;
using bytez.business.Dto.Product;
using bytez.entity.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace bytez.webui.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class ProductController : Controller
    {
        readonly IProductService _productService;
        readonly ICategoryService _categoryService;
        readonly IProductColorService _colorService;
        readonly private IBrandModelService _brandModelService;

        public ProductController(IProductService productService, ICategoryService categoryService, IProductColorService colorService, IBrandModelService brandModelService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _colorService = colorService;
            _brandModelService = brandModelService;
        }
        public async Task<IActionResult> Index()
        {
           
            List<Product> products = await _productService.GetProductsAsync();
            return View(products);
        }
        public async Task<IActionResult> Details(string id)
        {
            var product = await _productService.GetByIdAsync(id);

            return View(product);
        }
        public async Task<IActionResult> Create()
        {
            var category = await _categoryService.GetCategoryAsync();
            var brandModel = await _brandModelService.GetBrandsAsync();
            var color = await _colorService.GetProductColorsAsync();
            var product = new CreateProductDto();
            ProductCreateVM productCreateVM = new ProductCreateVM()
            {
                CreateProductDto = product,
                Category = category,
                BrandModel = brandModel,
                Color = color

            };

            return View(productCreateVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateVM model)
        {
            
            await _productService.Create(model);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(string id)
        {
            var product = await _productService.GetByIdAsync(id);
            var category = await _categoryService.GetCategoryAsync();
            var brandModel = await _brandModelService.GetBrandsAsync();
            var color = await _colorService.GetProductColorsAsync();
            if (product == null) return NotFound();
            UpdateProductDto productDto = new UpdateProductDto()
            {
                Title=product.Title,
                FilePath=product.FilePath,
                Avg=product.Avg,
                BrandsId=product.BrandsId, 
                CategoryId=product.CategoryId,
                ColorId=product.ColorId,
                ProfileProduct=product.ProfileProduct,
                IsProductLike=product.isProductLike,
                Price=product.Price,
                Stock=product.Stock,
                Higlist=product.Higlist,
                Description=product.Description

            };

            ProductUpdateVM productUpdateVM = new ProductUpdateVM()
            {
                UpdateProductDto = productDto,
                Category = category,
                BrandModel = brandModel,
                Color = color

            };
            return View(productUpdateVM);

        }
        [HttpPost]
        public async Task<IActionResult> Update(ProductUpdateVM model)
        {
            await _productService.Update(model);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null) return NotFound();
            await _productService.Delete(id);
            return RedirectToAction(nameof(Index));

        }
    }
}
