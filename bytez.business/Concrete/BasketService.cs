using bytez.business.Abstract;
using bytez.data.Abstract;
using bytez.entity.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using bytez.entity.Entities;

namespace bytez.business.Concrete
{
    public class BasketService : IBasketService
    {
      
        readonly private IProductReadRepository _productReadRepository;
        readonly private IProductWriteRepository _productWriteRepository;
        readonly private IBasketReadRepository _basketReadRepository;
        readonly private IBasketWriteRepository _basketWriteRepository;
        readonly private IProductBasketReadRepository  _productBasketReadRepository;
        readonly private IProductBasketWriteRepository  _productBasketWriteRepository;
        readonly private IHttpContextAccessor _httpContextAccessor;
        readonly private UserManager<AppUser> _userManager;
        public BasketService(IBasketReadRepository basketReadRepository,
                             IBasketWriteRepository basketWriteRepository,
                             UserManager<AppUser> userManager,
                             IProductBasketReadRepository  productBasketReadRepository,
                             IProductBasketWriteRepository  productBasketWriteRepository,
                             IProductWriteRepository productWriteRepository,
                             IProductReadRepository productReadRepository,
                              IHttpContextAccessor httpContextAccessor)
        {
            _basketReadRepository = basketReadRepository;
            _basketWriteRepository = basketWriteRepository;
            _productBasketReadRepository = productBasketReadRepository;
            _productBasketWriteRepository = productBasketWriteRepository;
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
            _httpContextAccessor = httpContextAccessor;

            _userManager = userManager;
        }

        public async Task<List<ProductBasket>> GetProductBasketAll()
        {
            var productBasket = await _productBasketReadRepository.GetAll()
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

            if (string.IsNullOrEmpty(username))
            {
                return false;
            }

            var user = await _userManager.Users
                .Include(u => u.Baskets)
                .FirstOrDefaultAsync(u => u.UserName == username);

            if (user == null)
            {
   
                return false;
            }

            var product = await _productReadRepository.GetByIdAsync(id);

            if (product == null)
            {
            
                return false;
            }

            var basket = await _basketReadRepository.GetSingleAsync(b => b.UserId == user.Id );

            if (basket == null)
            {
             
                basket = new Basket
                {
                    UserId = user.Id
                };

                await _basketWriteRepository.AddAsync(basket);
                await _basketWriteRepository.SaveAsync();

                var productBasket = await _productBasketReadRepository.GetSingleAsync(pb =>
                    pb.ProductId == product.Id &&
                    pb.BasketId == basket.Id);
                    
                    if (productBasket == null && product.Stock != null && product.Stock >= quantity)
                {
                    // Ürün sepete ekleniyor.
                    productBasket = new ProductBasket
                    {
                        BasketId = basket.Id,
                        ProductId = product.Id,
                        Quantity = quantity
                    };
                    product.Stock -= quantity;

                    await _productBasketWriteRepository.AddAsync(productBasket);
                    await _productBasketWriteRepository.SaveAsync();
                }
            }
            else
            {
                
                var productBasket = await _productBasketReadRepository.GetSingleAsync(pb =>
                    pb.ProductId == product.Id &&
                    pb.BasketId == basket.Id);
                if (productBasket != null && product.Stock != null && product.Stock >= quantity)
                {
                    productBasket.Quantity += quantity;
                    product.Stock -= quantity;

                    await _productBasketWriteRepository.SaveAsync();
                }else if(productBasket == null && product.Stock != null && product.Stock >= quantity)
                {
                    productBasket = new ProductBasket
                    {
                        BasketId = basket.Id,
                        ProductId = product.Id,
                        Quantity = quantity
                    };
                    product.Stock -= quantity;

                    await _productBasketWriteRepository.AddAsync(productBasket);
                    await _productBasketWriteRepository.SaveAsync();
                }

                await _productWriteRepository.SaveAsync();
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

               

                  


                 var  pb =  await  _productBasketReadRepository.GetByIdAsync(id);
                var product = await _productReadRepository.GetByIdAsync(pb.ProductId.ToString());

                      product.Stock += pb.Quantity;
                await _productWriteRepository.SaveAsync();
                        _productBasketWriteRepository.Remove(pb);
                     await  _productBasketWriteRepository.SaveAsync();

                    
                }
            
        }

        public async Task<Basket> GetBasketByid(string id)
        => await _basketReadRepository.GetByIdAsync(id);
    }
}
