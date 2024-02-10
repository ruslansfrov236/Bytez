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
    public class UpdateBlogDto
    {
        public string id { get; set; }
        [Required]
        public string? Title { get; set; }
        
        public string? Description { get; set; }
     
        public string? ContentInformation { get; set; }
        [Required]
        public bool isVideo { get; set; }
        public string? VideoPath { get; set; }
        [NotMapped]
        public IFormFile? Video { get; set; }
        public string? FilePath { get; set; }
        [NotMapped]
        public IFormFile? FormFile { get; set; }
    }}
