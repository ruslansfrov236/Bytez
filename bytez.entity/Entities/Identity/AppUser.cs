using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bytez.entity.Entities.Identity
{
    public class AppUser : IdentityUser
    {
        [Required(ErrorMessage = "This field is required.")]
     

        public string? NameSurname { get; set; }



        public ICollection<Basket> Baskets { get; set; }
    }
}
