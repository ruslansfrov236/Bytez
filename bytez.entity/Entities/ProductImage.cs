using bytez.entity.Entities.Customer;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace bytez.entity.Entities
{
    public class ProductImage:BaseEntity
    {
        public bool ShowCase { get; set; }
        public string FilePath {  get; set; }
        [NotMapped]
        public IFormFile File { get; set; } 
        public Guid ProductsId { get; set; }
        public Product? Products { get; set; }
    }
}
