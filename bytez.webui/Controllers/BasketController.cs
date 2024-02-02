using bytez.business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace bytez.webui.Controllers
{
    public class BasketController : Controller
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> AddBasket(string id , int quantity)
        {
            var basket = await _basketService.Add(id, quantity);
            return Ok(basket);
        }
    }
}
