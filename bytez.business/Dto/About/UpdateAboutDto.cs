﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bytez.business.Dto.About
{
    public class UpdateAboutDto
    {
        public string id { get; set; }      

        public string? FilePath { get; set; }
        [Required]
        public string? Description { get; set; }

        [NotMapped]
        public IFormFile? file { get; set; }
    }
}
