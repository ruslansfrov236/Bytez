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
     
        public string?  Description { get; set; }
     
        public string? ContentInformation { get; set; }
       
        public string? FilePath { get; set; }
    
        public bool isVideo { get; set; }
        public string? VideoPath { get; set; }

        [NotMapped]
        public IFormFile VideoFile { get; set; }
        [NotMapped]
        public IFormFile? FormFile { get; set; }


    }
}
