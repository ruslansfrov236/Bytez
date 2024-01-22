using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bytez.business.Dto.Header
{
    public class CreateHeaderDto
    {
        [Required]
        public string? Title { get; set; }

        [Required]
        public string? Descripton { get; set; }

        public string? FilePath { get; set; }

        [Required]
        [NotMapped]
        public IFormFile? File { get; set; }
    }
}
