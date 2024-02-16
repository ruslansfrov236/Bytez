

using bytez.business.Abstract;
using bytez.business.ViewModels.StockVM;
using bytez.entity.Entities;
using bytez.entity.Entities.Identity;
using bytez.webui.ViewModel;
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
        public StockController(IProductService productService, IProductColorService productColorService, ICategoryService categoryService, IBrandModelService brandModelService, IProductImageService productImageService , UserManager<AppUser> userManager , IWishlistService wishlistService , IHttpContextAccessor httpContextAccessor  )
        {
            _productService = productService;
            _productColorService = productColorService;
            _categoryService = categoryService;
            _brandModelService = brandModelService;
            _productImageService = productImageService;
            _userManager=userManager;
            _wishlistService=wishlistService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index(StockIndexVM model)
        {
          
                var username = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;

                // Retrieve user information
                AppUser user = await _userManager.Users
                    .Include(u => u.Wishlists)
                    .FirstOrDefaultAsync(u => u.UserName == username);

                var product = await _productService.GetWhereProduct(model);
                var products = await _productService.GetProductsAsync();
                var category = await _categoryService.GetCategoryAsync();
                var brandModel = await _brandModelService.GetBrandsAsync();
                var color = await _productColorService.GetProductColorsAsync();
                List<Wishlist> wishlists = await _wishlistService.GetWishlistsAllAsync();

              
                StockIndexVM stockIndex = new()
                {
                    Products = product,
                    Category = category,
                    BrandModel = brandModel,
                    Color = color,
                    Wishlists = wishlists,
                    AppUser = user
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
