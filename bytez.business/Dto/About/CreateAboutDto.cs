using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bytez.business.Dto.About
{
    public class CreateAboutDto
    {
        public string? FilePath { get; set; }

        public string? Description { get; set; }
        [NotMapped]
        public IFormFile file { get; set; }
    }
}
