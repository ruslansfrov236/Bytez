using bytez.business.Abstract;
using bytez.entity.Entities;
using bytez.entity.Entities.Identity;
using bytez.webui.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bytez.webui.Controllers
{
    public class HomeController : Controller
    {
        readonly private IHeaderService _headerService;
        readonly private IConnectionInfoService _connectionInfoService;
        readonly private IProductService _productService;
        readonly private UserManager<AppUser> _userManager;
        readonly private IWishlistService _wishlistService;
        readonly private IHttpContextAccessor _httpContextAccessor;
        public HomeController(IHeaderService headerService , IConnectionInfoService connectionInfoService, IProductService productService , IWishlistService wishlistService , UserManager<AppUser>  userManager , IHttpContextAccessor httpContextAccessor)
        {
            _headerService = headerService;
            _connectionInfoService = connectionInfoService;
            _productService = productService;
            _wishlistService = wishlistService;
            _httpContextAccessor = httpContextAccessor;
            _userManager= userManager;
        }

        public async Task<IActionResult> IndexAsync()
        {
            List<Header> header = await _headerService.GetHeaderListAsync();
            List<entity.Entities.ConnectionInfo> connectionInfos = await _connectionInfoService.GetAllConnections();
            List<Product> products = await _productService.FilterRecomneyeProduct();
            List<Wishlist> wishlists = await _wishlistService.GetWishlistsAllAsync();

            var username = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;
            AppUser user = await _userManager.Users
                    .Include(u => u.Wishlists)
                    .FirstOrDefaultAsync(u => u.UserName == username);
            HomeIndexVM model = new HomeIndexVM() {
                Headers = header,
                ConnectionInfos = connectionInfos,
                Products = products,
                Wishlists = wishlists.FirstOrDefault(),
                AppUser=user
                

            };
            return View(model) ;
        }
    }
}
