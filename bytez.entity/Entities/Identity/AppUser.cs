using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bytez.entity.Entities.Identity
{
    public class AppUser : IdentityUser<string>
    {

        public string? NameSurname { get; set; }

        public string? ConfirmPassword { get; set; }

        public ICollection<Basket> Baskets { get; set; }
    }
}
