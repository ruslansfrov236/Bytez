using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bytez.business.Dto.Cupon
{
    public class CreateCuponDto
    {
        public string Name { get; set; }

        public DateTime CuponTime { get; set; }
        public int Discount { get; set; }
    }
}
