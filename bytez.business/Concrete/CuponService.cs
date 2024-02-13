using bytez.business.Abstract;
using bytez.business.Dto.Cupon;
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
    public class CuponService : ICuponService
    {
        readonly private ICuponReadRepository _cuponReadRepository;
        readonly private ICuponWriteRepository _cuponWriteRepository;
        public CuponService(ICuponReadRepository  cuponReadRepository , ICuponWriteRepository cuponWriteRepository )
        {
            _cuponReadRepository= cuponReadRepository;
            _cuponWriteRepository= cuponWriteRepository;
        }
        public async Task<bool> Create(CreateCuponDto model)
        {
            Cupon cupon = new Cupon() { 
            Discount= model.Discount,
            Name= model.Name,
            CuponTime=model.CuponTime
            };
            await _cuponWriteRepository.AddAsync(cupon);
            await _cuponWriteRepository.SaveAsync();
            return true;
        }
        public async Task<bool>  CuponDeleteTime(string id)
        {
            var cupon = await _cuponReadRepository.GetByIdAsync(id);
            if (cupon.CuponTime == DateTime.UtcNow)
            {
                _cuponWriteRepository.Remove(cupon);
                await _cuponWriteRepository.SaveAsync();

            }
            return true;
        }
        public async Task<bool> Delete(string id)
        {
            var cupon = await _cuponReadRepository.GetByIdAsync(id);
            
                _cuponWriteRepository.Remove(cupon);
                await _cuponWriteRepository.SaveAsync();

            
            return true;
        }

        public async Task<Cupon> GetCuponById(string id)
       => await _cuponReadRepository.GetByIdAsync(id);

        public async Task<List<Cupon>> GetCupons()
        {
            List<Cupon> cupons = await _cuponReadRepository.GetAll().ToListAsync();

            return cupons;
        }

        public async Task<bool> Update(UpdateCuponDto model)
        {
            var cupon = await _cuponReadRepository.GetByIdAsync(model.Id);

            cupon.CuponTime = model.CuponTime;
            cupon.Discount = model.Discount;
            cupon.Name = model.Name;

            _cuponWriteRepository.Update(cupon);
            await _cuponWriteRepository.SaveAsync();
            return true;

        }
    }
}
