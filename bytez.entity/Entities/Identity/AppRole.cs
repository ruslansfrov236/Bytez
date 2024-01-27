using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace bytez.entity.Entities.Identity
{
    public class AppRole: IdentityRole
    {
       public RoleModel RoleModel { get; set; }
    }
}
    