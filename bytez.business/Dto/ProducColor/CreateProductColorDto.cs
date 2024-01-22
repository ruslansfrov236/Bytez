using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bytez.business.Dto.ProducColor
{
    public class CreateProductColorDto
    {
        [Required]
        public string? ColorName { get; set; }
    }
}
