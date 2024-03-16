using bytez.entity.Entities.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bytez.business.Dto.Product
{
    public class CreateProductDto
    {
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
        [Required]
        public string? FilePath { get; set; }
        [Required]

        public ICollection<SelectListItem>? Category { get; set; } // This should be singular

      
        [Required]
        public int Stock { get; set; }
        [Required]
        public Discount Discount { get; set; }

       
        [Required]
        [NotMapped]
        public IFormFile? ProductFile { get; set; }

        public string ProfileProduct { get; set; }

        public ICollection<SelectListItem>? Color { get; set; }

        public ICollection<SelectListItem>? Brands { get; set; }

        public ICollection<IFormFile>? Images { get; set; }
    }
}
