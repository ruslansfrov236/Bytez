using bytez.business.Abstract;
using bytez.business.Concrete;
using bytez.business.Dto.Cupon;
using bytez.entity.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bytez.webui.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class CuponController : Controller
    {
        readonly private ICuponService _cuponService;
      
        public CuponController(ICuponService cuponService)
        {
            _cuponService = cuponService;
        }
        [Authorize(Roles ="Admin,Manager")]
        public async Task<IActionResult> Index()
        {
            var cupon = await _cuponService.GetCupons();

            foreach (var c in cupon)
            {
                await _cuponService.CuponDeleteTime(c.Id.ToString());
            }
            return View(cupon);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        => View();
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateCuponDto model)
        {
            if (!ModelState.IsValid) return View(model);

                await _cuponService.Create(model);

            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(string id)
        {
            var cupon = await _cuponService.GetCuponById(id);
            if (cupon == null) return NotFound();
            UpdateCuponDto updateCuponDto = new UpdateCuponDto()
            {
                Id=cupon.Id.ToString(),
                CuponTime= cupon.CuponTime,
                Discount=cupon.Discount,
                Name=cupon.Name
            };
            return View(updateCuponDto);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Update(UpdateCuponDto model)
        {
            if (!ModelState.IsValid) return View(model);
            var cupon = await _cuponService.GetCuponById(model.Id);
            if (cupon == null) return BadRequest();
            await _cuponService.Update(model);

            return RedirectToAction(nameof(Index));

        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var cupon = await _cuponService.GetCuponById(id);
            if (cupon == null) return BadRequest();

            await _cuponService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
