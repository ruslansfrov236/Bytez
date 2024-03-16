using bytez.entity.Entities.Customer;
using bytez.entity.Entities.Enum;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace bytez.entity.Entities
{
    public class Product : BaseEntity
    {
        public string? Title { get; set; }
           
        public decimal Price { get; set; }
        [ForeignKey("ColorId")]
        public Guid ColorId { get; set; }
        [ForeignKey("CategoryId")]
        public Guid CategoryId { get; set; }
        public string? Description { get; set; }
        [ForeignKey("BrandsId")]
        public Guid BrandsId { get; set; }
        public ProductRam ProductRam {  get; set; }
        public ProductMemory ProductMemory { get; set; }


        public string? Higlist { get; set; }
        [NotMapped]
        public IFormFile?  ProductFile { get; set; }

         public string?  ProfileProduct { get; set; }
        public string? FilePath { get; set; }
        public Category? Category { get; set; }
        public int Stock { get; set; }
        public Discount Discount { get; set; }   
     

        public Wishlist? Wishlist { get; set; }
        public ICollection<BrandModel>? Brands { get; set; } 
        public ICollection<ProductImage>? ProductImages { get; set; }
        public ICollection<ProductColor>? Color { get; set; }
        public ICollection<ProductBasket>? ProductBaskets { get; set; }




    }
}
