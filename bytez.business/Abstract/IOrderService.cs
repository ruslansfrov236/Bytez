using bytez.business.Dto.Order;
using bytez.entity.Entities;

namespace bytez.business.Abstract
{
    public interface IOrderService
    {
        Task<List<Order>> GetOrderAsync();
     

        Task<bool> Create(CreateOrderDto model);
 
 
      
        Task<bool> Delete(string id);
    }
}
