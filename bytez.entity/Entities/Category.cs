using bytez.entity.Entities.Customer;

namespace bytez.entity.Entities
{
    public class Category:BaseEntity
    {
        public string Name { get; set; }    

        public int Count { get; set; }
        public  ICollection<Product>? Product { get; set; }   
    }
}
