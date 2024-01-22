using bytez.entity.Entities.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bytez.entity.Entities
{
    public class Highlist:BaseEntity
    {
        public string? Memory { get; set; }
        public string? Camera { get; set; }
        public string? Processor { get; set; }


        public string? Delivery { get; set; }
        public string? BankCard { get; set; }

    }
}
