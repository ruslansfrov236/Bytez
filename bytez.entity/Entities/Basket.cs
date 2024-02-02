using bytez.entity.Entities.Customer;
using bytez.entity.Entities.Identity;

namespace bytez.entity.Entities
{
    public class Basket:BaseEntity
    {


        public string UserId { get; set; }  

        public AppUser? User { get; set; }
        public Guid OrdersId { get; set; }
        public Order? Orders { get; set; }
        public ICollection<ProductBasket>?  ProductBaskets { get; set; }
    }
}
