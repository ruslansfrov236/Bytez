using bytez.entity.Entities.Customer;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace bytez.entity.Entities
{
    public  class BrandModel:BaseEntity
    {
        public string Name { get; set; }
        public string FilePath { get; set; }
        [NotMapped]
        public IFormFile? formFile { get; set; }
        public Product? Product { get; set; }
    }
}
