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

        [ForeignKey("ColorId")]
        public Guid ColorId { get; set; }
        [Required]

        [ForeignKey("CategoryId")]
        public Guid CategoryId { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [ForeignKey("BrandsId")]
        public Guid BrandsId { get; set; }
        [Required]
        public ProductRam ProductRam { get; set; }
        [Required]
        public ProductMemory ProductMemory { get; set; }
        [Required]
        public string? Higlist { get; set; }

        public string? FilePath { get; set; }

        public ICollection<SelectListItem>? Category { get; set; } // This should be singular
       
        public bool IsProductLike { get; set; }
        [Required]
        public int Stock { get; set; }
        [Required]
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
