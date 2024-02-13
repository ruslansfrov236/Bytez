using bytez.entity.Entities.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bytez.entity.Entities
{
    public class OrderComponent:BaseEntity
    {
        public Guid CuponId { get; set; }

        public Cupon Cupon { get; set; }

        public Guid DeliveryId {  get; set; }   
        public Delivery Delivery { get; set; }

        public decimal TotalPrice { get; set; }

        public Guid OrderId { get; set; }

        public Order Order { get; set; }
    }
}
