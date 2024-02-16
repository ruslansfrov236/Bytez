using bytez.business.Dto.Product;
using bytez.entity.Entities;
using bytez.entity.Entities.Identity;

namespace bytez.business.ViewModels.StockVM
{
    public class StockIndexVM
    {
        public ProductWhereDto? ProductWhereDto { get; set; }

        public List<Product> Products { get; set; }

        public List<Category>? Category { get; set; }
        public List<BrandModel>? BrandModel { get; set; }
        public List<ProductColor>? Color { get; set; }
        public List< Wishlist> Wishlists { get; set; }
        public AppUser AppUser { get; set; }

    }
}
