using W = bytez.entity.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bytez.entity.Entities.Enum;

namespace bytez.business.Dto.Product
{
    public  class ProductWhereDto
    {
        public List<W::Product> Products { get; set; }
        public int? minValue {  get; set; }  
        public int? maxValue {  get; set; }
       
        public string? CategoryId { get; set; }  
        public string? ColorId { get; set; }  
        public string? BrandsId { get; set; }
        
        public Discount? Discount { get; set; }

    }
}
