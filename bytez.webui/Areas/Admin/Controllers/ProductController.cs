using bytez.business.Abstract;
using bytez.business.Dto.Product;
using bytez.business.ViewModels.ProductVM;
using bytez.entity.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        [Authorize(Roles = "Admin , Manager")]
        public async Task<IActionResult> Index()
        {

            List<Product> products = await _productService.GetProductsAsync();
            return View(products);
        }
        [Authorize(Roles = "Admin , Manager")]
        public async Task<IActionResult> Details(string id)
        {
            var product = await _productService.GetByIdAsync(id);

            return View(product);
        }
        [Authorize(Roles = "Admin ")]
        public async Task<IActionResult> Create()
        {
            var category = await _categoryService.GetCategoryAsync();
            var brandModel = await _brandModelService.GetBrandsAsync();
            var color = await _colorService.GetProductColorsAsync();

            ViewBag.Category = category.Select(a => new SelectListItem() { Text = a.Name, Value = a.Id.ToString() }).ToList();
            ViewBag.Brands = brandModel.Select(a => new SelectListItem() { Text = a.Name, Value = a.Id.ToString() }).ToList();
            ViewBag.Color = color.Select(a => new SelectListItem() { Text = a.ColorName, Value = a.Id.ToString() }).ToList();
            
            return View(new CreateProductDto());
        }

        [HttpPost]
        [Authorize(Roles = "Admin ")]
        public async Task<IActionResult> Create(CreateProductDto model)
        {
            if (!ModelState.IsValid) return View(model);
            await _productService.Create(model);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Authorize(Roles = "Admin ")]

        public async Task<IActionResult> Update(string id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null) return NotFound();

            var category = await _categoryService.GetCategoryAsync() ;
            var brandModel = await _brandModelService.GetBrandsAsync();
            var color = await _colorService.GetProductColorsAsync() ;

            UpdateProductDto productDto = new UpdateProductDto()
            {
                id = product.Id.ToString(),
                Title = product.Title,
                FilePath = product.FilePath,
                BrandsId = product.BrandsId,
                CategoryId = product.CategoryId,
                ColorId = product.ColorId,
                Category= category.Select(a=> new SelectListItem() { Text=a.Name , Value=a.Id.ToString()}).ToList(),
                Brands= brandModel.Select(a => new SelectListItem() { Text = a.Name, Value = a.Id.ToString() }).ToList(),
                Color= color.Select(a => new SelectListItem() { Text = a.ColorName, Value = a.Id.ToString() }).ToList(),
                ProfileProduct = product.ProfileProduct,
                Price = product.Price,
                Stock = product.Stock,
                Higlist = product.Higlist,
                Description = product.Description
            };

           

            return View(productDto);
        }

        [HttpPost]
        [Authorize(Roles = "Admin ")]
        public async Task<IActionResult> Update(UpdateProductDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
                await _productService.Update(model);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [Authorize(Roles = "Admin ")]
        public async Task<IActionResult> Delete(string id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null) return NotFound();
            await _productService.Delete(id);
            return RedirectToAction(nameof(Index));

        }
    }
}
