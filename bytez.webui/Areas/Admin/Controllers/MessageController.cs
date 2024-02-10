using bytez.business.Abstract;
using bytez.entity.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bytez.webui.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Authorize(Roles ="Manager , Admin")]
    public class MessageController : Controller
    {
        readonly private IMessageService _messageService;
        public MessageController(IMessageService messageService)
        {
            _messageService= messageService;

        }
        public async Task<IActionResult> Index()
        {
            List<Message> messages = await _messageService.GetMessagesAllAsync();
            return View(messages);
        }
        public async Task<IActionResult> Details(string id)
        {
            var message = await _messageService.GetMessagesById(id);
            if (message == null) return NotFound();
            return View(message);
        }
    }
}
