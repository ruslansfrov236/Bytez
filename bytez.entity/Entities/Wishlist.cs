using bytez.entity.Entities.Customer;
using bytez.entity.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bytez.entity.Entities
{
    public class Wishlist:BaseEntity
    {

        public bool isWishlist { get; set; }
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }

        public Guid ProductId { get; set; }
        public Product? Product { get; set; }
    }
}
