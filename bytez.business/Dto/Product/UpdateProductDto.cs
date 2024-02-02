using bytez.entity.Entities;
using C= bytez.entity.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;

namespace bytez.business.Dto.Product
{
    public class UpdateProductDto
    {
        public string id { get; set; }
        public string? Title { get; set; }

        public decimal Price { get; set; }

        [ForeignKey("ColorId")]
        public Guid ColorId { get; set; }

        [ForeignKey("CategoryId")]
        public Guid CategoryId { get; set; }

        public string Description { get; set; }

        [ForeignKey("BrandsId")]
        public Guid BrandsId { get; set; }

        public ProductRam ProductRam { get; set; }
        public ProductMemory ProductMemory { get; set; }
        public string? Higlist { get; set; }
        public string? FilePath { get; set; }

        public ICollection<SelectListItem>? Category { get; set; } // This should be singular
       
        public bool IsProductLike { get; set; }

        public int Stock { get; set; }

        public Discount Discount { get; set; }

        public float Avg { get; set; }
        [NotMapped]
        public IFormFile? ProductFile { get; set; }

        public string ProfileProduct { get; set; }


        public ICollection<SelectListItem>? Color { get; set; }

        public ICollection<SelectListItem>? Brands { get; set; }

        public ICollection<IFormFile>? Images { get; set; }

    }
}
