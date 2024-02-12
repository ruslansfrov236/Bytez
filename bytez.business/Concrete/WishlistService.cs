using bytez.business.Abstract;
using bytez.business.Dto.Wishlist;
using bytez.data.Abstract;
using bytez.data.Concrete;
using bytez.entity.Entities;
using bytez.entity.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bytez.business.Concrete
{

    public class WishlistService : IWishlistService
    {
        readonly private IProductReadRepository _productReadRepository;
        readonly private IProductWriteRepository _productWriteRepository;
        readonly private IWishlistReadRepository _wishlistReadRepository;
        readonly private IWishlistWriteRepository _wishlistWriteRepository;
        readonly private IHttpContextAccessor _httpContextAccessor;
        readonly private UserManager<AppUser> _userManager;

        public WishlistService(IWishlistReadRepository wishlistReadRepository,
                               IWishlistWriteRepository wishlistWriteRepository,
                               IProductReadRepository productReadRepository,
                               IProductWriteRepository productWriteRepository,
                               IHttpContextAccessor httpContextAccessor,
                               UserManager<AppUser> userManager
                               )
        {
            _wishlistReadRepository = wishlistReadRepository;
            _wishlistWriteRepository = wishlistWriteRepository;
            _userManager = userManager;
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<bool> Add(CreateWishlistDto model)
        {
            var username = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;

            if (!string.IsNullOrEmpty(username))
            {
                AppUser user = await _userManager.Users
                    .Include(u => u.Wishlists)
                    .FirstOrDefaultAsync(u => u.UserName == username);
                var product = await _productReadRepository.GetByIdAsync(model.ProductId);
                if (user != null && product != null)
                {
                    model.isWishlist = true;
                    Wishlist wishlist = new Wishlist()
                    {

                        isWishlist = model.isWishlist,
                        AppUserId = user.Id,
                        ProductId = product.Id
                    };

                    await _wishlistWriteRepository.AddAsync(wishlist); ;
                    await _wishlistWriteRepository.SaveAsync();
                    return true;

                }

                return true;
            }

            return false;
        }


        public async Task<List<Wishlist>> GetWishlistsAllAsync()
        {
            List<Wishlist> wishlists = await _wishlistReadRepository.GetAll()
                                                                    .Include(a => a.Product)
                                                                    .Include(a => a.AppUser)
                                                                    .Select(a => new Wishlist()
                                                                    {
                                                                        
                                                                        Product= a.Product,
                                                                        AppUserId=a.AppUserId,
                                                                        isWishlist=a.isWishlist,
                                                                        ProductId=a.ProductId,
                                                                        Id=a.Id

                                                                    })

                                                                    .ToListAsync();

            return wishlists;
        }

        public async Task Remove(string id)
        {
            var username = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;
            if (!string.IsNullOrEmpty(username))
            {

                AppUser user = await _userManager.Users
                    .Include(u => u.Wishlists)
                    .FirstOrDefaultAsync(u => u.UserName == username);

                var wishlist = await _wishlistReadRepository.GetByIdAsync(id);

                if (wishlist != null)
                {

                    _wishlistWriteRepository.Remove(wishlist);
                    await _wishlistWriteRepository.SaveAsync();

                }

            }

        }

    }
}
