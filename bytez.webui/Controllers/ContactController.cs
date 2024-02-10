using bytez.business.Abstract;
using bytez.business.Dto.ContactCall;
using bytez.business.Dto.Messages;
using bytez.entity.Entities;
using bytez.webui.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace bytez.webui.Controllers
{
    public class ContactController : Controller
    {
        readonly private IContactCallService _contactCallService;
        readonly private IContactWallService _contactWallService;
        readonly private IMessageService _messageService;
        readonly private IEmailService _emailService;

        public ContactController(IContactCallService contactCallService,
                                 IContactWallService contactWallService,
                                 IMessageService messageService,
                                 IEmailService emailService)
        {
            _contactCallService = contactCallService;
            _contactWallService = contactWallService;
            _emailService = emailService;
            _messageService = messageService;
        }
        public async Task<IActionResult> Index()
        {
            var contactCall = await _contactCallService.GetContactCallAllAsync();
            var contacWall = await _contactWallService.GetContactWallAllAsync();
            var email = await _emailService.GetEmailAllAsync();
            var me = new CreateMessageDto();
            ContactIndexVM contactIndexVM = new ContactIndexVM()
            {
                ContactCall = contactCall.FirstOrDefault(),
                ContactWall = contacWall.FirstOrDefault(),
                Emails = email,
                CreateMessageDto = me

            };

            return View(contactIndexVM);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateMessageDto model)
        {
            if (!ModelState.IsValid) return View(model);
          
            await _messageService.Create(model);

            return StatusCode((int)HttpStatusCode.Created);
        }
    }
}
