using bytez.business.Dto.Cupon;
using bytez.entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bytez.business.Abstract
{
    public interface ICuponService
    {
        Task<List<Cupon>> GetCupons();
        Task<Cupon> GetCuponById(string id);
        Task<bool> Create(CreateCuponDto model);
        Task<bool> Update(UpdateCuponDto model);
        Task<bool> CuponDeleteTime(string id);
        Task<bool> Delete(string id);

    }
}
