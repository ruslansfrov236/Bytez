using bytez.entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bytez.business.Abstract
{
    public interface IOrderComponentService
    {
        Task<List<OrderComponent>> GetOrderComponents();
        Task<OrderComponent> GetOrderComponentById(string id);
        Task <bool> Delete(string id);
        Task<bool> Create( string id);
    }
}
