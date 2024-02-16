using bytez.business.Abstract;
using bytez.data.Abstract;
using bytez.entity.Entities;
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

        public OrderComponentService(IOrderComponentReadRepository orderComponentReadRepository,
                            IOrderComponentWriteRepository orderComponentWriteRepository)
        {
            _orderComponentReadRepository = orderComponentReadRepository;
            _orderComponentWriteRepository = orderComponentWriteRepository;
        }

        public async Task<bool> Create(string id)
        {
            var oc = await _orderComponentReadRepository.GetByIdAsync(id);
            OrderComponent orderComponent = new OrderComponent()
            {
        
                TotalPrice = oc.Order.Basket.ProductBaskets
                        .Sum(a => (a.Quantity * a.Product.Price) + oc.Delivery.Price - ((a.Quantity * a.Product.Price) - ((a.Quantity * a.Product.Price) * oc.Cupon.Discount / 100)))



            };
            await _orderComponentWriteRepository.AddAsync(orderComponent);
            await _orderComponentWriteRepository.SaveAsync();
            return true;
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
