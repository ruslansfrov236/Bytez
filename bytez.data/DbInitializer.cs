using bytez.entity.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bytez.entity.Entities.Enum;

namespace bytez.data
{
    public class DbInitializer
    {
        
        public async static Task SeedAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            foreach (var role in Enum.GetValues(typeof(RoleModel)))
            {
                if (!await roleManager.RoleExistsAsync(role.ToString()))
                {
                    await roleManager.CreateAsync(new IdentityRole
                    {
                        Name = role.ToString(),
                    });
                }
            }
            if (await userManager.FindByNameAsync("admin") == null)
            {
                var user = new AppUser
                {
                  
                    NameSurname = "admin",
                    UserName = "admin",
                    Email = "admin@app.com"
                };
                var password = "876.adDFaFR@#$";
                var result = await userManager.CreateAsync(user, password);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        throw new Exception(error.Description);
                    }
                }
                await userManager.AddToRoleAsync(user, RoleModel.Admin.ToString());
            }
            if (await userManager.FindByNameAsync("manager") == null)
            {
                var user = new AppUser
                {
                    
                    NameSurname = "manager",
                    UserName = "manager",
                    Email = "manager@bytez.com",
                   
                };
                var password = "876.RFaFR@#$";
                var result = await userManager.CreateAsync(user, password);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        throw new Exception(error.Description);
                    }
                }
                await userManager.AddToRoleAsync(user, RoleModel.Manager.ToString());
            }
        }
    }
}
