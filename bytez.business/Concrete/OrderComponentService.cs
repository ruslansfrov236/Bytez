using bytez.business.Abstract;
using bytez.data.Abstract;
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

    public class OrderComponentService : IOrderComponentService
    {
        readonly private IOrderComponentReadRepository _orderComponentReadRepository;
        readonly private IOrderComponentWriteRepository _orderComponentWriteRepository;
        readonly private UserManager<AppUser> _userManager;
        readonly private IHttpContextAccessor _httpContextAccessor;
        public OrderComponentService(IOrderComponentReadRepository orderComponentReadRepository,
                            IOrderComponentWriteRepository orderComponentWriteRepository,
                            UserManager<AppUser> userManager ,
                            IHttpContextAccessor httpContextAccessor
                            )
        {
            _orderComponentReadRepository = orderComponentReadRepository;
            _orderComponentWriteRepository = orderComponentWriteRepository;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> Create(string id)
        {
            var username = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;

            if(!string.IsNullOrEmpty( username ) )
            {
                AppUser? user = await _userManager.Users
                              .Include(u => u.OrderComponents)
                              .FirstOrDefaultAsync(u => u.UserName == username);

                var oc = await _orderComponentReadRepository.GetByIdAsync(id);
                if (oc != null)
                {
                    OrderComponent orderComponent = new OrderComponent()
                    {
                        AppUserId=Guid.Parse(user.Id),

                        TotalPrice = oc.Order.Basket.ProductBaskets
                         .Sum(a => (a.Quantity * a.Product.Price) + oc.Delivery.Price - ((a.Quantity * a.Product.Price)
                         - ((a.Quantity * a.Product.Price) * oc.Cupon.Discount / 100)))



                    };
                    await _orderComponentWriteRepository.AddAsync(orderComponent);
                    await _orderComponentWriteRepository.SaveAsync();
                }
             
                return true;
            }

            return false;


        }

        public async Task<bool> Delete(string id)
        {
            var orderComponent = await _orderComponentReadRepository.GetByIdAsync(id);

            _orderComponentWriteRepository.Remove(orderComponent);
            await _orderComponentWriteRepository.SaveAsync();

            return true;
        }


        public async Task<OrderComponent> GetOrderComponentById(string id)
        => await _orderComponentReadRepository.GetByIdAsync(id);

        public async Task<List<OrderComponent>> GetOrderComponents()
        {
            List<OrderComponent> orderComponents = await _orderComponentReadRepository
                .GetAll()
                .Include(a => a.Order)
                .Include(a => a.Cupon)
                .Include(a => a.Delivery)
                
                .ToListAsync();

            return orderComponents;
        }



    }
}
