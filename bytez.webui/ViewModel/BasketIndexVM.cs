using bytez.entity.Entities;

namespace bytez.webui.ViewModel
{
    public class BasketIndexVM
    {
     public   Basket Baskets { get; set; }  

    public List<Product> Products { get; set; }
 
    public List<ProductBasket> ProductBaskets { get; set; }

    public List<Order> Order { get; set; }

    public Delivery Delivery { get; set; }
    public List<Cupon> Cupons { get; set; }
    }
}
