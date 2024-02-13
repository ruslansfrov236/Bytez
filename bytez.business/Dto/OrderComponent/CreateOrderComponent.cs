
using b = bytez.entity.Entities;
namespace bytez.business.Dto.OrderComponent
{
    public class CreateOrderComponent
    {
        public Guid CuponId { get; set; }

        public b.Cupon Cupon { get; set; }

        public Guid DeliveryId { get; set; }
        public b.Delivery Delivery { get; set; }

        public decimal TotalPrice { get; set; }

        public Guid OrderId { get; set; }

        public b.Order Order { get; set; }
    }
}
