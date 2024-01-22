using bytez.business.Dto.Header;
using bytez.entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bytez.business.Abstract
{
    public interface IHeaderService
    {
        Task<List<Header>> GetHeaderListAsync();
        Task<Header> GetHeaderByIdAsync(string id);
        Task<bool> Create(CreateHeaderDto model);
        Task<bool> UpdateAsync(UpdateHeaderDto model);
        Task<bool> Delete(string id);
    }
}
