using bytez.entity.Entities;
using bytez.entity.Entities.Identity;

namespace bytez.webui.ViewModel
{
    public class WishlistIndexVM
    {
        public List<Wishlist> Wishlists { get; set; }   

        public AppUser AppUser { get; set; }
    }
}
