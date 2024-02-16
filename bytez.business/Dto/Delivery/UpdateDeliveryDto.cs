using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bytez.business.Dto.Delivery
{
    public class UpdateDeliveryDto
    {
        public string Id { get; set; }
        [Required]
        public int Price { get; set; }
    }
}
