using bytez.entity.Entities;
using C=bytez.entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bytez.business.Dto.Product
{
    public class ProductCreateVM
    {
        public List<C::Category>? Category { get; set; }
        public List<C::BrandModel>? BrandModel { get; set; }
        public List<ProductColor>? Color { get; set; }

        public CreateProductDto? CreateProductDto { get; set; }
    }
}
