

using bytez.business.Abstract;
using bytez.business.Dto.Login;
using bytez.data.Context;
using bytez.entity.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace bytez.business.Concrete
{
    public class AppUserService : IAppUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly AppDbContext _context;

        public AppUserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, AppDbContext context)
        {
            _userManager = userManager;
            _context = context;
            _signInManager = signInManager;
        }
        public async Task LoginAsync(Login model)
        {
           
            var emailUser = await _userManager.FindByEmailAsync(model.Email);
            AppUser? user = new();

            if (emailUser != null)
            {
                user = emailUser;
            }

            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                await _signInManager.SignInAsync(user, false);

            }

        }


        public async Task LogOutAsync()
       => await _signInManager.SignOutAsync();

        public async Task<AppUser> RegistrationAsync(CreateRegistrationDto model)
        {
            AppUser user = new AppUser()
            {
                UserName = model.Name,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return user;
            }


            return default;
        }

    }
}
