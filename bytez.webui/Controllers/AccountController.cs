using Ardalis.GuardClauses;
using bytez.business.Abstract;
using bytez.business.Dto.Login;
using bytez.entity.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace bytez.webui.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class AccountController : Controller
    {
        readonly private IAppUserService _appUserService;
        readonly private UserManager<AppUser> _userManager;
        readonly private SignInManager<AppUser> _signInManager;
        public AccountController(IAppUserService appUserService , UserManager<AppUser> userManager , SignInManager<AppUser> signInManager  )
        {
            _appUserService = appUserService;
            _userManager=userManager;
            _signInManager=signInManager;
        }
        [HttpGet]
        public IActionResult Login ()
       => View( new LoginDto() );
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Login(LoginDto model)
        {
           
            if (!ModelState.IsValid)
            {
                return View(model);
            }
          await  _appUserService.LoginAsync(model);

            return Redirect(model.ReturnUrl ?? "~/");


        }

        [HttpGet]
        public IActionResult Registration()
      => View();
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration(CreateRegistrationDto model )
        {
            await _appUserService.RegistrationAsync(model);
            return RedirectToAction(nameof(Login));
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOut()
        {
            await _appUserService.LogOutAsync();
            return RedirectToAction(nameof(Login));
        }
    }
}
