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

        public BasketController(IBasketService basketService , IProductService productService)
        {
            _basketService = basketService;
            _productService= productService;
        }
        public async Task<IActionResult> Index()
        {
            var basket = await _basketService.GetBasketAll();
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
        [HttpPost]
        public async Task<IActionResult> AddBasket(string id , int quantity)
        {
             await _basketService.Add(id, quantity);
            return StatusCode((int)HttpStatusCode.Created);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {

         var basket=  _basketService.Remove(id);


            return Ok(basket);
        }
    }
}
