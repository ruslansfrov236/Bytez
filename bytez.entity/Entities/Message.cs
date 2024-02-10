using bytez.entity.Entities.Customer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bytez.entity.Entities
{
    public class Message:BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required , DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required, DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        [Required]
        public string MessageInfo { get; set; }  
}
}
