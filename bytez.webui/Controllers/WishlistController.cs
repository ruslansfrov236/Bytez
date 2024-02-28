using bytez.business.Abstract;
using bytez.business.Dto.Wishlist;
using bytez.entity.Entities;
using bytez.entity.Entities.Identity;
using bytez.webui.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace bytez.webui.Controllers
{
    [Authorize(Roles = "User , Manager , Admin ")]
    public class WishlistController : Controller
    {
        readonly private IWishlistService _wishlistService;
        readonly private IHttpContextAccessor _httpContextAccessor;
        readonly private UserManager<AppUser> _userManager;

        public WishlistController(IWishlistService wishlistService , UserManager<AppUser> userManager , IHttpContextAccessor httpContextAccessor )
        {
            _wishlistService= wishlistService;
            _httpContextAccessor=httpContextAccessor;
            _userManager=userManager;
        }
        public async Task<IActionResult> Index()
        {
             var username = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;
        AppUser user = await _userManager.Users
                .Include(u => u.Wishlists)
                .FirstOrDefaultAsync(u => u.UserName == username);
        
            List<Wishlist> wishlists = await _wishlistService.GetWishlistsAllAsync();

                WishlistIndexVM vm = new WishlistIndexVM()
                {
                    Wishlists = wishlists,
                    AppUser=user

                };
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Create (CreateWishlistDto model)
        {
            if (!User.Identity.IsAuthenticated)
            {
               
                string returnUrl = Request.Path + Request.QueryString;
                return RedirectToAction("Login", "Account", new { returnUrl });
            }
            var wishlist = await _wishlistService.Add(model);
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete( string id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                // ReturnUrl'i tutmak için
                string returnUrl = Request.Path + Request.QueryString;
                return RedirectToAction("Login", "Account", new { returnUrl });
            }
            await _wishlistService.Remove(id);

            return Ok();
        }
    }
}
