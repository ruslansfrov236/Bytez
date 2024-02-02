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
<<<<<<< HEAD

        public async Task<IActionResult> Login(LoginDto model)
=======
        public async Task<IActionResult> Login(Login model )
>>>>>>> 651f6b607ec294a06289e76cd145cc075226798a
        {
           
            if (!ModelState.IsValid)
            {
                return View(model);
            }
          await  _appUserService.LoginAsync(model);

            return Redirect(model.ReturnUrl ?? "~/");


<<<<<<< HEAD
=======
                    if (result.Succeeded)
                    {
                        if (userRoles.Contains("Admin") || userRoles.Contains("Manager"))
                        {
                           
                                return Redirect(model.ReturnUrl );
                           
                           
                        }
                        else if (userRoles.Contains("User"))
                        {
                            return Redirect("~/");
                        }
                    }
                }
            }
            else
            {
                ModelState.AddModelError("Email", "Invalid email or password.");
            }

            return View(model);
>>>>>>> 651f6b607ec294a06289e76cd145cc075226798a
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
