

using bytez.business.Abstract;
using bytez.business.Dto.Login;
using bytez.data.Context;
using bytez.entity.Entities;
using bytez.entity.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace bytez.business.Concrete
{
    public class AppUserService : IAppUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly AppDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager; 
        public AppUserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, AppDbContext context, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _context = context;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public async Task LoginAsync(Login model)
        {
           
            var emailUser = await _userManager.FindByEmailAsync(model.Email);
            AppUser? user = new();

            if (emailUser != null )
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
          
            foreach (var roleName in new[] { RoleModel.User.ToString() })
            {
                if (!await _roleManager.RoleExistsAsync(roleName))
                {
                    await _roleManager.CreateAsync(new IdentityRole { Name = roleName });
                }
            }
            if (model.Password != model.ConfirmPassword)
            {
                return default;
            }

            var user = new AppUser
            {
                NameSurname = model.Name,
                UserName = model.Name,
                Email = model.Email
            };

            await _userManager.AddToRoleAsync(user, RoleModel.User.ToString());
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return default;
            }
            
            return user;
          

        }


    }
}
