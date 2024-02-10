using bytez.business.Dto.ContactCall;
using bytez.business.Dto.ContactWall;
using bytez.entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bytez.business.Abstract
{
    public interface IContactCallService
    {
        Task<List<ContactCall>> GetContactCallAllAsync();
        Task<ContactCall> ContactCallById(string id);
        Task<bool> Create(CreateContactCallDto model);
        Task<bool> Update(UpdateContactCallDto model);
        Task<bool> Delete(string id);
    }
}
