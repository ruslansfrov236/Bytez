using Ardalis.GuardClauses;
using bytez.business.Abstract;
using bytez.business.Dto.Login;
using bytez.entity.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bytez.webui.Controllers
{
    public class AccountController : Controller
    {
        readonly private IAppUserService _appUserService;
        readonly private UserManager<AppUser> _userManager;
        readonly private SignInManager<AppUser> _signInManager;
       readonly private IHttpContextAccessor _httpContextAccessor;
		public AccountController(IAppUserService appUserService , UserManager<AppUser> userManager , SignInManager<AppUser> signInManager , IHttpContextAccessor httpContextAccessor)
        {
            _appUserService = appUserService;
            _userManager=userManager;
            _signInManager=signInManager;
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpGet]
        public async Task< IActionResult> Login()
        {
			var username = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;



			AppUser? user = await _userManager.Users.FirstOrDefaultAsync
		   (u => u.UserName == username);
            if (user != null)
            {
				await _appUserService.LogOutAsync();
				
			}
			return View(new LoginDto());


		}
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDto model )
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
