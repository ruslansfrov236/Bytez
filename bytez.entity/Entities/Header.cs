using bytez.entity.Entities.Customer;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bytez.entity.Entities
{
    public  class Header:BaseEntity
    {
        public string? Title { get; set; }   

        public string? Descripton { get; set; }

        public string? FilePath { get; set; }

        [NotMapped]
        public IFormFile? File { get; set; }
    }
}
