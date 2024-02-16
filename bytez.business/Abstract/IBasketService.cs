using bytez.entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bytez.business.Abstract
{
    public interface IBasketService
    {
        Task<List<ProductBasket>> GetProductBasketAll();
        Task<List<Basket>> GetBasketAll();
        Task<Basket> GetBasketByid(string id);
        Task<bool> Add(string id , int quantity);
        Task Remove(string id);
    }
}
