using bytez.business.Dto.ContactWall;
using bytez.entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bytez.business.Abstract
{
     public interface IContactWallService
    {
        Task<List<ContactWall>> GetContactWallAllAsync();
        Task<ContactWall> ContactWallById(string id);
        Task<bool> Create(CreateContactWallDto model);
        Task<bool> Update(UpdateContactWallDto model);
        Task<bool> Delete(string id);
    }
}
