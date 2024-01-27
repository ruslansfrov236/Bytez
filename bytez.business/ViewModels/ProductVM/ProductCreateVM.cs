using bytez.entity.Entities;
using C = bytez.entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bytez.business.Dto.Product;

namespace bytez.business.ViewModels.ProductVM
{
    public class ProductCreateVM
    {
        public List<Category>? Category { get; set; }
        public List<BrandModel>? BrandModel { get; set; }
        public List<ProductColor>? Color { get; set; }

        public CreateProductDto? CreateProductDto { get; set; }
    }
}
