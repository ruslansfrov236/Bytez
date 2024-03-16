using C = bytez.entity.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using bytez.entity.Entities.Enum;

namespace bytez.business.Dto.Product
{
    public class UpdateProductDto
    {
        public string id { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]

        public decimal Price { get; set; }
        [Required]

       
        public Guid ColorId { get; set; }
        [Required]

      
        public Guid CategoryId { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
       
        public Guid BrandsId { get; set; }
        [Required]
        public ProductRam ProductRam { get; set; }
        [Required]
        public ProductMemory ProductMemory { get; set; }
        [Required]
        public string? Higlist { get; set; }

        public string? FilePath { get; set; }

        public List<SelectListItem>? Category { get; set; } 
       
   
        [Required]
        public int Stock { get; set; }
        [Required]
        public Discount Discount { get; set; }

      
        
        public IFormFile? ProductFile { get; set; }

        public string? ProfileProduct { get; set; }


        public ICollection<SelectListItem>? Color { get; set; }

        public ICollection<SelectListItem>? Brands { get; set; }

        public ICollection<IFormFile>? Images { get; set; }

    }
}
