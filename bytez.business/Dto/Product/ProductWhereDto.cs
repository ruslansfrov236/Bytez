using bytez.entity.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bytez.business.Dto.Product
{
    public  class ProductWhereDto
    {
        public decimal minValue {  get; set; }  
        public decimal maxValue {  get; set; }
        public int avg { get; set; }
         public ProductColor? ProductColor {  get; set; }
        public List<SelectListItem>? Category { get; set; }
        public List<SelectListItem>? Brand { get; set; }
        public string? CategoryId { get; set; }  
        public string? ColorId { get; set; }  
        public string? BrandsId { get; set; }

    }
}
