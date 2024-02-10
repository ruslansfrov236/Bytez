using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bytez.business.Dto.Wishlist
{
    public class CreateWishlistDto
    {
        public string ProductId { get; set; }

        public bool isWishlist { get; set; }
    }
}
