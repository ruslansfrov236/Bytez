using bytez.business.Dto.Delivery;
using bytez.entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bytez.business.Abstract
{
    public interface IDeliveryService
    {
        Task<List<Delivery>> GetDeliveriesAll();
        Task<Delivery> GetDeliveriesById(string id);
        Task<bool> Create(CreateDeliveryDto model);
        Task<bool> Update(UpdateDeliveryDto model);
        Task<bool> Delete(string id);
    }
}
