using bytez.business.Abstract;
using bytez.data.Abstract;
using bytez.entity.Entities.Identity;
using e=bytez.entity.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using bytez.entity.Entities;
using System.Linq.Expressions;
using System.Security.AccessControl;

namespace bytez.business.Concrete
{
    public class BasketService : IBasketService
    {
        readonly private IOrderReadRepository _orderReadRepository;
        readonly private IOrderWriteRepository _orderWriteRepository;
        readonly private IProductReadRepository _productReadRepository;
        readonly private IProductWriteRepository _productWriteRepository;
        readonly private IBasketReadRepository _basketReadRepository;
        readonly private IBasketWriteRepository _basketWriteRepository;
        readonly private IProductBasketReadRepository _productBasketRead;
        readonly private IProductBasketWriteRepository _productBasketWrite;
        readonly private IHttpContextAccessor _httpContextAccessor;
        readonly private UserManager<AppUser> _userManager;
        public BasketService(IBasketReadRepository basketReadRepository,
                             IBasketWriteRepository basketWriteRepository,
                             UserManager<AppUser> userManager,
                             IProductBasketReadRepository productBasketRead,
                             IProductBasketWriteRepository productBasketWrite,
                             IProductWriteRepository productWriteRepository,
                             IProductReadRepository productReadRepository,
                              IHttpContextAccessor httpContextAccessor)
        {
            _basketReadRepository = basketReadRepository;
            _basketWriteRepository = basketWriteRepository;
            _productBasketRead = productBasketRead;
            _productBasketWrite = productBasketWrite;
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
            _httpContextAccessor = httpContextAccessor;

            _userManager = userManager;
        }

        public async Task<List<ProductBasket>> GetProductBasketAll()
        {
            var productBasket = await _productBasketRead.GetAll()
                                                        .Select(pr => new ProductBasket()
                                                        {
                                                            Id = pr.Id,
                                                            Basket = pr.Basket,
                                                            Product = pr.Product,
                                                            BasketId = pr.BasketId,
                                                            CreatedDate = pr.CreatedDate,
                                                            Quantity = pr.Quantity,
                                                            ProductId = pr.ProductId,
                                                            UpdatedDate = pr.UpdatedDate
                                                        }).ToListAsync();

            return productBasket;
        }
        public async Task<List<Basket>> GetBasketAll()
        {
            var username = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;

           
            
                AppUser? user = await _userManager.Users
                    .Include(u => u.Baskets)
                    .FirstOrDefaultAsync(u => u.UserName == username);
            
                var basket = await _basketReadRepository.GetAll()
                .Include(a => a.User)
                .Include(a => a.ProductBaskets)
                .Include(a => a.Orders)
                .Where(a => a.UserId == user.Id)
                .Select(a => new Basket()
                {
                    Id = a.Id,
                    CreatedDate = a.CreatedDate,
                    UpdatedDate = a.UpdatedDate,
                    UserId = a.UserId,
                    User = a.User,
                    ProductBaskets = a.ProductBaskets
                        .Select(pr => new ProductBasket()
                        {
                            Id = pr.Id,
                            Quantity = pr.Quantity,
                            Product = pr.Product,
                            ProductId = pr.ProductId,
                            BasketId = pr.BasketId
                        }).ToList()
                })
                .ToListAsync();
            
            return basket;
        }

        public async Task<bool> Add(string id, int quantity)
        {
            var username = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;

            if (!string.IsNullOrEmpty(username))
            {
                AppUser user = await _userManager.Users
                    .Include(u => u.Baskets)
                    .FirstOrDefaultAsync(u => u.UserName == username);

                var product = await _productReadRepository.GetByIdAsync(id);

                if (product != null)
                {

                    var userBasket = await _basketReadRepository.GetSingleAsync(b => b.UserId == user.Id);

                    if (userBasket == null)
                    {
                        userBasket = new Basket
                        {
                            UserId = user.Id,

                        };

                        await _basketWriteRepository.AddAsync(userBasket);
                        await _basketWriteRepository.SaveAsync();


                        var productBasket = await _productBasketRead.GetSingleAsync(c => c.ProductId.ToString() == id);

                        if (productBasket == null && product.Stock != null&& product.Stock >= productBasket.Quantity)
                        {
                        
                            productBasket = new ProductBasket
                            {
                                BasketId = userBasket.Id,
                                ProductId = product.Id,
                                Quantity = quantity
                            };
                            product.Stock -= quantity;
                        }


                        if (productBasket!=null &&   product.Stock != null && product.Stock >= productBasket.Quantity)
                        {
                            productBasket.Quantity++;
                            product.Stock-=quantity;


                            await _productWriteRepository.SaveAsync();
                        }

                        await _productBasketWrite.AddAsync(productBasket);
                        await _productBasketWrite.SaveAsync();
                    }
                }
            }


            return true;
        }

        public async Task Remove(string id)
        {
            var username = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;

            if (!string.IsNullOrEmpty(username))
            {
                AppUser user = await _userManager.Users
                    .Include(u => u.Baskets)
                    .FirstOrDefaultAsync(u => u.UserName == username);

                var product = await _productReadRepository.GetByIdAsync(id);

                if (product != null)
                {

                    var userBasket = await _basketReadRepository.GetSingleAsync(b => b.UserId == user.Id);

                    if (userBasket != null)
                    {
                    

                        _basketWriteRepository.Remove(userBasket);
                        await _basketWriteRepository.SaveAsync();

                    }
                }
            }
        }

        public async Task<Basket> GetBasketByid(string id)
        => await _basketReadRepository.GetByIdAsync(id);
    }
}
