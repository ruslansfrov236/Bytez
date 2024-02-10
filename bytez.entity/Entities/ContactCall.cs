using bytez.entity.Entities.Customer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bytez.entity.Entities
{
    public class ContactCall:BaseEntity
    {
        [Required]
        public string Title {  get; set; }  
        [Required]
        public string Description { get; set; }
        [Required , DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
    }
}
