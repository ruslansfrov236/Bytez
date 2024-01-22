using bytez.business.Dto.Product;
using bytez.entity.Entities;

namespace bytez.webui.ViewModel
{
    public class StockIndexVM
    {
        public ProductWhereDto? ProductWhereDto { get; set; }

        public List<Product> Products { get; set; } 

        public List<Category>? Category { get; set; }
        public List<BrandModel>? BrandModel { get; set; }
        public List<ProductColor>? Color { get; set; }
         
    }
}
