using bytez.entity.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bytez.business.Dto.Email
{
    public class UpdateEmailDto
    {
        public string id {  get; set; }
       
        [Required, DataType(DataType.EmailAddress)]
        public string? EmailAddress { get; set; }
    }
}
