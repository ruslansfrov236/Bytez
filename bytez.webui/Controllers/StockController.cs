

using bytez.business.Abstract;
using bytez.business.Dto.Product;
using bytez.business.ViewModels.StockVM;
using bytez.entity.Entities;
using bytez.entity.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bytez.webui.Controllers
{
    [Authorize(Roles = "User , Manager , Admin ")]
    public class StockController : Controller
    {
        readonly private IProductService _productService;
        readonly private IProductColorService _productColorService;
        readonly private ICategoryService _categoryService;
        readonly private IBrandModelService _brandModelService;
        readonly private IProductImageService _productImageService;
        readonly private UserManager<AppUser> _userManager;
        readonly private IWishlistService _wishlistService;
        readonly private IHttpContextAccessor _httpContextAccessor;
        public StockController(IProductService productService, IProductColorService productColorService, ICategoryService categoryService, IBrandModelService brandModelService, IProductImageService productImageService , UserManager<AppUser> userManager , IWishlistService wishlistService  )
        {
            _productService = productService;
            _productColorService = productColorService;
            _categoryService = categoryService;
            _brandModelService = brandModelService;
            _productImageService = productImageService;
            _userManager=userManager;
            _wishlistService=wishlistService;
        }

        public async Task<IActionResult> Index(ProductWhereDto model)
        {
            try
            {
                var username = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;

                // Retrieve user information
                AppUser user = await _userManager.Users
                    .Include(u => u.Wishlists)
                    .FirstOrDefaultAsync(u => u.UserName == username);

                // Retrieve products and other related data
                var product = await _productService.GetWhereProduct(model);
                var products = await _productService.GetProductsAsync();
                var category = await _categoryService.GetCategoryAsync();
                var brandModel = await _brandModelService.GetBrandsAsync();
                var color = await _productColorService.GetProductColorsAsync();
                List<Wishlist> wishlists = await _wishlistService.GetWishlistsAllAsync();

                // Create the view model
                StockIndexVM stockIndex = new()
                {
                    Products = product ?? products,
                    Category = category,
                    BrandModel = brandModel,
                    Color = color,
                    Wishlists = wishlists.FirstOrDefault(),
                    AppUser = user
                };

                return View(stockIndex);
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
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
