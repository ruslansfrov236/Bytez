using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bytez.business.Dto.BrandModel
{
    public class UpdateBrandModelDto
    {
        public string? id {  get; set; } 
        public string? Name { get; set; }
        public string? FilePath { get; set; }
        [NotMapped]
        public IFormFile? formFile { get; set; }

    }
}
