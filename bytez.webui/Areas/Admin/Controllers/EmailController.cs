using bytez.business.Abstract;
using bytez.business.Dto.Email;
using bytez.entity.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bytez.webui.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class EmailController : Controller
    {
        readonly private IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }
        [Authorize(Roles = "Admin , Manager")]
        public async Task<ActionResult> Index()
        {
            List<Email> email = await _emailService.GetEmailAllAsync();
            return View(email);
        }

        [Authorize(Roles = "Admin , Manager")]
        public async Task<ActionResult> Details(string id)
        {
            var email = await _emailService.GetEmailByIdAsync(id);
            if (email == null) return NotFound();

            return View(email);
        }

        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create()
      => View(new CreateEmailDto());

        // POST: EmailController/Create
        [HttpPost]
        [Authorize(Roles = "Admin ")]
        public async Task<ActionResult> Create(CreateEmailDto model)
        {
            if (!ModelState.IsValid) return View(model);
            await _emailService.Create(model);

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin ")]
        public async Task<ActionResult> Update(string id)
        {
            var email = await _emailService.GetEmailByIdAsync(id);
            if (email == null) return NotFound();
            UpdateEmailDto updateEmailDto = new UpdateEmailDto()
            {
                EmailAddress=email.EmailAddress,

            };

            return View(updateEmailDto);
        }
        [HttpPost]
        [Authorize(Roles = "Admin ")]
        public async Task<ActionResult> Update(string id, UpdateEmailDto model)
        {
            if (!ModelState.IsValid) return View(model);
            if (id != model.id) return BadRequest();
            await _emailService.Update(model);

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [Authorize(Roles = "Admin ")]
        public async Task<ActionResult> Delete (string id)
        {
            var email = await _emailService.GetEmailByIdAsync(id);
            if (email == null) return NotFound();
            return RedirectToAction(nameof(Index));
        }



    }
}
