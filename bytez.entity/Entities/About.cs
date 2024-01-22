using bytez.entity.Entities.Customer;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace bytez.entity.Entities
{
    public class About:BaseEntity
    {

        public string? FilePath { get; set; }    

        public string? Description { get; set; }
        [NotMapped]
        public IFormFile file { get; set; } 
    }
}
