using bytez.business.Abstract;
using bytez.business.Dto.Delivery;
using bytez.data.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bytez.webui.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class DeliveryController : Controller
    {
        readonly private IDeliveryService _deliveryService;

        public DeliveryController(IDeliveryService deliveryService)
        {
            _deliveryService = deliveryService;
        }
        public async Task<IActionResult> Index()
        {
            var delivery = await _deliveryService.GetDeliveriesAll();

            return View(delivery);
        }
        public async Task<IActionResult> Details(string id)
        {
            var delivery = await _deliveryService.GetDeliveriesById(id);
            if (delivery == null) return NotFound();

            return View(delivery);
        }
        public async Task<IActionResult> Create()
        => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateDeliveryDto model)
        {
            if (!ModelState.IsValid) return View(model);

            await _deliveryService.Create(model);
           return  RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(string id)
        {
            var delivery = await _deliveryService.GetDeliveriesById(id);

            UpdateDeliveryDto updateDeliveryDto = new UpdateDeliveryDto()
            {
                Id=delivery.Id.ToString(),
                Price=delivery.Price

            };

            return View(updateDeliveryDto);

        }
        [HttpPost]
        public async Task<IActionResult> Update (UpdateDeliveryDto model)
        {
            if (!ModelState.IsValid) return View(model);
            await _deliveryService.Update(model);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Delete (string id)
        {
            var delivery = await _deliveryService.GetDeliveriesById(id);
            if (delivery == null) return BadRequest();
            await _deliveryService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
