using bytez.business.Abstract;
using bytez.business.Dto.Delivery;
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
    public class DeliveryService : IDeliveryService
    {
        readonly private IDeliveryReadRepository _deliveryReadRepository;
        readonly private IDeliveryWriteRepository _deliveryWriteRepository;

        public DeliveryService(IDeliveryReadRepository deliveryReadRepository , IDeliveryWriteRepository deliveryWriteRepository)
        {
            _deliveryReadRepository=deliveryReadRepository;
            _deliveryWriteRepository=deliveryWriteRepository;
        }
        public async Task<bool> Create(CreateDeliveryDto model)
        {
            Delivery delivery = new Delivery() { 
            Price=model.Price
            
            };
            await _deliveryWriteRepository.AddAsync(delivery);
            await _deliveryWriteRepository.SaveAsync();
            return true;
        }

        public async Task<bool> Delete(string id)
        {
            var delivery = await _deliveryReadRepository.GetByIdAsync(id);
            _deliveryWriteRepository.Remove(delivery);
            await _deliveryWriteRepository.SaveAsync();
            return true;
        }

        public async Task<List<Delivery>> GetDeliveriesAll()
        {
            var delivery = await _deliveryReadRepository.GetAll().ToListAsync();
            return delivery;
        }

        public async Task<Delivery> GetDeliveriesById(string id)
       => await _deliveryReadRepository.GetByIdAsync(id);

        public async Task<bool> Update(UpdateDeliveryDto model)
        {
            var delivery = await _deliveryReadRepository.GetByIdAsync(model.Id);

            delivery.Price = model.Price;

            _deliveryWriteRepository.Update(delivery);
            await _deliveryWriteRepository.SaveAsync();

            return true;
        }
    }
}
