using bytez.entity.Entities;
using bytez.entity.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bytez.business.Abstract
{
    public interface IAppRoleService
    {
        Task<List<AppUser>> GetAppUserROLE();
        Task GetAppUpdateROLE(RoleModel roleModel, string id);



    }
}
