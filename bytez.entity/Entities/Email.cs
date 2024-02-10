using bytez.entity.Entities.Customer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bytez.entity.Entities
{
    public class Email:BaseEntity
    {

         
   
        [Required, DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }
    }
}
