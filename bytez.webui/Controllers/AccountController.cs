using bytez.business.Abstract;
using bytez.business.Dto.Login;
using Microsoft.AspNetCore.Mvc;

namespace bytez.webui.Controllers
{
    public class AccountController : Controller
    {
        readonly private IAppUserService _appUserService;

        public AccountController(IAppUserService appUserService)
        {
            _appUserService = appUserService;
        }
        [HttpGet]
        public IActionResult Login()
        => View();
        [HttpPost]
        public async  Task<IActionResult> Login( Login model)
        {
            await _appUserService.LoginAsync(model);
            return RedirectToAction("/");
        }
        [HttpGet]
        public IActionResult Registration()
      => View();
        [HttpPost]
        public async Task<IActionResult> Registration(CreateRegistrationDto model )
        {
            await _appUserService.RegistrationAsync(model);
            return RedirectToAction(nameof(Login));
            
        }

        public async Task<IActionResult> LogOut()
        {
            await _appUserService.LogOutAsync();
            return RedirectToAction("/");
        }
    }
}
