using bytez.entity.Entities;


using bytez.business.Dto.Product;

namespace bytez.business.ViewModels.ProductVM
{
    public class ProductUpdateVM
    {
        public List<Category>? Category { get; set; }
        public List<BrandModel>? BrandModel { get; set; }
        public List<ProductColor>? Color { get; set; }

        public UpdateProductDto? UpdateProductDto { get; set; }
    }
}
