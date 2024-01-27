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
        public IActionResult Login (string? ReturnUrl = null)
       => View(new Login() { ReturnUrl=ReturnUrl});
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login model , string? ReturnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var emailUsers = await _userManager.FindByEmailAsync(model.Email);

            if (emailUsers != null)
            {
                if (await _userManager.CheckPasswordAsync(emailUsers, model.Password))
                {
                    var userRoles = await _userManager.GetRolesAsync(emailUsers);
                    var result = await _signInManager.PasswordSignInAsync(emailUsers, model.Password, true, false);

                    if (result.Succeeded)
                    {
                        if (userRoles.Contains("Admin") || userRoles.Contains("Manager"))
                        {
                           
                                return Redirect(ReturnUrl );
                           
                           
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
            return RedirectToAction("~/");
        }
    }
}
