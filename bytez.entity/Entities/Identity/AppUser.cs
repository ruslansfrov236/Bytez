﻿using Microsoft.AspNetCore.Identity;
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
        public ICollection<Wishlist> Wishlists { get; set; }

        public ICollection<OrderComponent> OrderComponents { get; set; }
        
     
        public ICollection<Comment> Comments { get; set; }
    }
}
