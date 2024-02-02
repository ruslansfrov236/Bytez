using bytez.business.Abstract;
using bytez.business.Dto.Header;
using bytez.entity.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bytez.webui.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class HeaderController : Controller
    {

        readonly private IHeaderService _headerService;

        public HeaderController(IHeaderService headerService)
        {
            _headerService = headerService;
        }
        [Authorize(Roles = "Admin , Manager")]
        public async Task<IActionResult> Index()
        {
            List<Header> headers = await _headerService.GetHeaderListAsync();

            return View(headers);
        }
        [Authorize(Roles = "Admin , Manager")]
        public async Task<IActionResult> Details(string id)
        {
            var header = await _headerService.GetHeaderByIdAsync(id);
            if (header == null) return NotFound();


            return View(header);
        }
        [Authorize(Roles = "Admin ")]
        public async Task<IActionResult> Create()
        => View();
        [HttpPost]
        [Authorize(Roles = "Admin ")]
        public async Task<IActionResult> Create(CreateHeaderDto model)
        {
            if (!ModelState.IsValid) return View(model);

            await _headerService.Create(model);

            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Admin ")]
        public async Task<IActionResult> Update(string id)
        {
            var header = await _headerService.GetHeaderByIdAsync(id);

            if (header == null) return NotFound();
            UpdateHeaderDto updateHeaderDto = new()
            {
                Title = header.Title,
                Description = header.Descripton,
                File = header.File,
                FilePath = header.FilePath

            };

            return View(updateHeaderDto);

        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(UpdateHeaderDto model)
        {
            await _headerService.UpdateAsync(model);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Authorize(Roles = "Admin ")]
        public async Task<IActionResult> Delete(string id)
        {
            var header = await _headerService.GetHeaderByIdAsync(id);
            if (header == null) return NotFound();

            await _headerService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
