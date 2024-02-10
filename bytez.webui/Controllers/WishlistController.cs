using bytez.business.Abstract;
using bytez.business.Dto.Wishlist;
using bytez.entity.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace bytez.webui.Controllers
{
    [Authorize(Roles = "User , Manager , Admin ")]
    public class WishlistController : Controller
    {
        readonly private IWishlistService _wishlistService;

        public WishlistController(IWishlistService wishlistService)
        {
            _wishlistService= wishlistService;
        }
        public async Task<IActionResult> Index()
        {
            List<Wishlist> wishlists = await _wishlistService.GetWishlistsAllAsync();
            return View(wishlists);
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
            return Ok(wishlist);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromRoute] string id)
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
