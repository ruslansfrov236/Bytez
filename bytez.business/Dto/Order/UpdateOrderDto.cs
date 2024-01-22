using bytez.entity.Entities;

namespace bytez.business.Dto.Order
{
    public class UpdateOrderDto
    {
        public string? Id { get; set; }
        public List<ProductBasket>? ProductBasket { get; set; }
        public string? Address { get; set; }
        public string? OrderCode { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? Description { get; set; }
    }
}
