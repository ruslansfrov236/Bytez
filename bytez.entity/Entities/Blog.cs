using bytez.entity.Entities.Customer;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bytez.entity.Entities
{
    public class Blog:BaseEntity
    {
        [Required]
        public string? Title { get; set; }
        [Required]
        public string ? Description { get; set; }
        [Required]
        public string? ContentInformation { get; set; }
        [Required]
        public string? FilePath { get; set; }
        [NotMapped]
        public IFormFile? File { get;}
    }
}
