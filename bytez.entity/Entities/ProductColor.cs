using bytez.entity.Entities.Customer;

namespace bytez.entity.Entities
{
    public class ProductColor:BaseEntity
    {
        public string? ColorName {  get; set; } 
        
        public Product? Product { get; set; }    
    }
}
