﻿using bytez.entity.Entities.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bytez.entity.Entities
{
    public class Cupon:BaseEntity
    {
        public string Name { get; set; }    

    
        public DateTime CuponTime { get; set; }
        public int Discount { get; set; }   
    }
}
