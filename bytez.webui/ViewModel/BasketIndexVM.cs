﻿using bytez.entity.Entities;

namespace bytez.webui.ViewModel
{
    public class BasketIndexVM
    {
     public   Basket Baskets { get; set; }  

    public Product Products { get; set; }
 
    public ProductBasket ProductBaskets { get; set; }

    public List<Order> Order { get; set; }

    public Delivery Delivery { get; set; }
    public List<Cupon> Cupons { get; set; }
    }
}
