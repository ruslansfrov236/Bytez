using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bytez.business.Dto.Blog
{
    public class CreateBlogDto
    {
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public string? ContentInformation { get; set; }
        [Required]
        public string? FilePath { get; set; }
        [NotMapped]
        public IFormFile? File { get; }
    }
}
