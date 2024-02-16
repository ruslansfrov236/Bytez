using bytez.entity.Entities;
using B = bytez.entity.Entities;

namespace bytez.business.Dto.Order
{
    public class UpdateOrderDto
    {
        public string? Id { get; set; }

        public string? Description { get; set; }
        public string? Address { get; set; }
        public string? OrderCode { get; set; }
        public Guid BasketId { get; set; }
        public Basket? Basket { get; set; }

        public B::OrderComponent OrderComponent { get; set; }
    }
}
