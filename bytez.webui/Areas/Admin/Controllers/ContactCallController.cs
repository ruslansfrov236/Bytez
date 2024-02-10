using bytez.business.Abstract;
using bytez.business.Dto.ContactCall;
using bytez.entity.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bytez.webui.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class ContactCallController : Controller
    {
        readonly private IContactCallService _contactCallService;

        public ContactCallController(IContactCallService contactCallService)
        {
            _contactCallService = contactCallService;
        }
        [Authorize(Roles = "Manager , Admin")]
        public async Task<IActionResult> Index()
        {
            List<ContactCall> contactCalls = await _contactCallService.GetContactCallAllAsync();

            return View(contactCalls);
        }
       
        [Authorize(Roles = "Admin ")]
        public async Task<IActionResult> Create()
        {
            return View();
           
        }
        [HttpPost]
        [Authorize(Roles = " Admin")]
        public async Task<IActionResult> Create(CreateContactCallDto model)
        {
            if (!ModelState.IsValid) return View(model);
           
            await _contactCallService.Create(model);
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = " Admin")]
        public async Task<IActionResult> Update(string id)
        {
            var contactCall = await _contactCallService.ContactCallById(id);
            if (contactCall == null) return NotFound();

            UpdateContactCallDto updateContactCallDto = new UpdateContactCallDto()
            { Description = contactCall.Description, Phone = contactCall.Phone , Title=contactCall.Title};
            return View(updateContactCallDto);
        }
        [HttpPost]
        [Authorize(Roles = " Admin")]
        public async Task<IActionResult> Update(string id , UpdateContactCallDto model)
        {
            if (!ModelState.IsValid) return View(model);
            if (id != model.id) return BadRequest();
            await _contactCallService.Update(model);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [Authorize(Roles = " Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            var contactCall = await _contactCallService.ContactCallById(id);
            if (contactCall == null) return NotFound();
            await _contactCallService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
