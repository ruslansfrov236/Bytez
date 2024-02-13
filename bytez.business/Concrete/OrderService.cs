using bytez.business.Abstract;
using bytez.business.Dto.Order;
using bytez.data.Abstract;
using bytez.entity.Entities;
using Microsoft.EntityFrameworkCore;
namespace bytez.business.Concrete
{
    public class OrderService : IOrderService
    {
        readonly private IOrderReadRepository _orderReadRepository;
        readonly private IOrderWriteRepository _orderWriteRepository;

        public OrderService(IOrderReadRepository orderReadRepository, IOrderWriteRepository orderWriteRepository)
        {
            _orderReadRepository = orderReadRepository;
            _orderWriteRepository = orderWriteRepository;
        }



        public async Task<bool> Create(CreateOrderDto model)
        {
            var orderCode = (new Random().NextDouble() * 10000).ToString();
            orderCode = orderCode.Substring(orderCode.IndexOf(",") + 1, orderCode.Length - orderCode.IndexOf(",") - 1);
            Order order = new Order()
            {
                Name = model.Name,
                Address = model.Address,
                Description = model.Description,
                OrderCode = orderCode,
                
                
                
            };

            await _orderWriteRepository.AddAsync(order);
            await _orderWriteRepository.SaveAsync();

            return true;
        }

        public async Task<bool> Delete(string id)
        {
            var order = await _orderReadRepository.GetByIdAsync(id);

            _orderWriteRepository.Remove(order);
            await _orderWriteRepository.SaveAsync();
            return true;
        }

    








        public async Task<List<Order>> GetOrderAsync()
        {
            var  orders = await  _orderReadRepository.GetAll()
                                            .Include(o => o.Basket)
                                            .ThenInclude(b => b.User)
                                            .Include(o => o.Basket)
                                            .ThenInclude(b => b.ProductBaskets)
                                            .ThenInclude(bi => bi.Product)
                                        
                                             .ToListAsync();



            //var orderItems = await  orders.Select(o => new OrderItems()
            //{
            //    Id = Id,
            //    CreatedDate = o.CreatedDate,
            //    OrderCode = o.OrderCode,
            //    TotalPrice = o.Basket.ProductBaskets.Sum(bi => bi.Product.Price * bi.Quantity),
            //    UserName = o.Basket.User.UserName
            //}).ToList();

            return orders;
        }

        
    }
}
