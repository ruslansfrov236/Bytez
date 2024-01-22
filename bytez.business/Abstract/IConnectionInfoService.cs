using bytez.business.Dto.ConnectionInfo;
using bytez.entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bytez.business.Abstract
{
    public interface IConnectionInfoService
    {

     
        Task<List<ConnectionInfo>> GetAllConnections(); 
        Task<ConnectionInfo> GetConnectionInfoById(string id);
        Task<bool> Create(CreateConnectionInfoDto model);
        Task<bool> Update(UpdateConnectionInfoDto model);
        Task<bool> Delete(string id);
    }
}
