using bytez.business.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace bytez.webui.Controllers
{
    [Authorize(Roles = "User , Manager , Admin ")]
    public class BasketController : Controller
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }
        public async Task<IActionResult> Index()
        {
            var basket = await _basketService.GetBasketAll();

            return View(basket);
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
