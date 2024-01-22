using bytez.entity.Entities.Customer;

namespace bytez.entity.Entities
{
    public class Order:BaseEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Address { get; set; }
        public string? OrderCode { get; set; }

        public Basket? Basket { get; set; }
      
          
    }
}
