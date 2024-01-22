using bytez.entity.Entities.Customer;

namespace bytez.entity.Entities
{
    public class ProductBasket:BaseEntity
    {
        public Guid ProductId { get; set; }
        public Product? Product { get; set; }
        public int Quantity { get; set; }
        public Guid BasketId { get; set; }  
        public Basket? Basket { get; set; }  
    }
}
