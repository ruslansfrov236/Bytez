using bytez.business.Abstract;
using bytez.business.Dto.ContactWall;
using bytez.entity.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bytez.webui.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class ContactWallController : Controller
    {
        readonly private IContactWallService _contactWallService;

        public ContactWallController(IContactWallService contactWallService)
        {
            _contactWallService= contactWallService;
        }
        [Authorize(Roles = "Manager , Admin")]
        public async Task<IActionResult> Index()
        {
            List<ContactWall> contactWalls = await _contactWallService.GetContactWallAllAsync();
            return View(contactWalls);
        }
        [Authorize(Roles = "Manager , Admin")]
        public async Task<IActionResult> Details(string id)
        {
            var contactWalls = await _contactWallService.ContactWallById(id);
            if (contactWalls == null) return NotFound();

            return View(contactWalls);
        }
        [Authorize(Roles = " Admin")]
        public async Task<IActionResult> Create()
        {
           return View(new CreateContactWallDto());
                    
            
            
        }
        [HttpPost]
        [Authorize(Roles = " Admin")]
        public async Task<IActionResult> Create(CreateContactWallDto model)
        {
            if (!ModelState.IsValid) return View(model);
            await _contactWallService.Create(model);

            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = " Admin")]
        public async Task<IActionResult>  Update (string id)
        {
            var contactWall = await _contactWallService.ContactWallById(id);
            if (contactWall == null) return NotFound();
            UpdateContactWallDto updateContactWallDto = new UpdateContactWallDto() { Description=contactWall.Description , Title=contactWall.Title };

            return View(updateContactWallDto);

        }
        [HttpPost]
        [Authorize(Roles = " Admin")]
        public async Task<IActionResult> Update(string id , UpdateContactWallDto model)
        {
            if (!ModelState.IsValid) return View(model);
            if (id != model.Id) return BadRequest();

            await _contactWallService.Update(model);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [Authorize(Roles = " Admin")]
        public async Task<IActionResult>  Delete (string id)
        {
            var contactWall = await _contactWallService.ContactWallById(id);
            if (contactWall == null) return NotFound();
            await _contactWallService.Delete(id);
            return RedirectToAction(nameof(Index));

        }
    }
}
