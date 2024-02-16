using bytez.business.Abstract;
using bytez.entity.Entities;
using bytez.webui.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace bytez.webui.Controllers
{
    [Authorize(Roles = "User , Manager , Admin ")]
    public class BasketController : Controller
    {
        private readonly IBasketService _basketService;

        private readonly IProductService _productService;

        private readonly ICuponService _cuponService;

        private readonly IDeliveryService _deliveryService;

        private readonly IOrderComponentService _orderComponentService;
        private readonly IOrderService _orderService;
        public BasketController(IBasketService basketService ,
                                IProductService productService,
                                ICuponService  cuponService,
                                IDeliveryService deliveryService,
                                IOrderComponentService orderComponentService,
                                IOrderService orderService)
        {
            _basketService = basketService;
            _productService= productService;
            _cuponService = cuponService;
            _deliveryService= deliveryService;
            _orderComponentService = orderComponentService;
            _orderService = orderService;

        }

        public async Task<IActionResult> Index()
        {
            var basket = await _basketService.GetBasketAll();
            var cupon = await _cuponService.GetCupons();
            var delivery = await _deliveryService.GetDeliveriesAll();
            var order = await _orderService.GetOrderAsync();
            foreach (var or in order)
            {
                
                    await _orderComponentService.Create(or.Id.ToString());
                
            }


            var orderCreateComponent = await _orderComponentService.GetOrderComponents();

            var product = await _productService.GetProductsAsync();
            List<ProductBasket> productBasket = await _basketService.GetProductBasketAll();
            BasketIndexVM basketIndex = new BasketIndexVM()
            {
                Baskets = basket.FirstOrDefault(),
                Products = product.FirstOrDefault(),
                ProductBaskets = productBasket.FirstOrDefault()
               
            };

            return View(basketIndex);
        }

        [HttpGet]
        public async Task<IActionResult> CuponFilter(string name)
        {
            var cupon = await _cuponService.GetWhereCupon(name);

            return Ok(cupon);
        }
        [HttpPost]
        public async Task<IActionResult> AddBasket(string id , int quantity)
        {
             await _basketService.Add(id, quantity);
            return StatusCode((int)HttpStatusCode.Created);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var basket = await _basketService.GetBasketByid(id);

            if (basket == null)
            {
                
                return NotFound($"Basket with ID {id} not found.");
            }

            await _basketService.Remove(id);

            
            return Ok($"Basket with ID {id} has been deleted.");
        }

    }
}
