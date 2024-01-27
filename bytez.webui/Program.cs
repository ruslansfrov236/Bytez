
using bytez.business;
using bytez.data;
using bytez.entity.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static bytez.data.ServiceRegistration;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();
builder.Services.AddDataRegistration();
builder.Services.AddBusinessRegistration();
builder.Services.AddCookieRegistration();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();
#region Admin Controller
#region  Color
app.MapAreaControllerRoute(

    name: "areas",
    areaName: "admin",
    pattern: "admin/color/update/{id}",
    defaults: new { Controller = "ProductColor", Action = "Update" }
    );
app.MapAreaControllerRoute(

    name: "areas",
    areaName: "admin",
    pattern: "admin/color/create",
    defaults: new { Controller = "ProductColor", Action = "Create" }
    );
app.MapAreaControllerRoute(

    name: "areas",
    areaName: "admin",
    pattern: "admin/color/details/{id}",
    defaults: new { Controller = "ProductColor", Action = "Details" }
    );
app.MapAreaControllerRoute(

    name: "areas",
    areaName: "admin",
    pattern: "admin/color",
    defaults: new { Controller = "ProductColor", Action = "Index" }
    );
#endregion
#region About

app.MapAreaControllerRoute(

    name: "areas",
    areaName: "admin",
    pattern: "admin/about/update/{id}",
    defaults: new { Controller = "About", Action = "Update" }
    );
app.MapAreaControllerRoute(

    name: "areas",
    areaName: "admin",
    pattern: "admin/about/create",
    defaults: new { Controller = "About", Action = "Create" }
    );
app.MapAreaControllerRoute(

    name: "areas",
    areaName: "admin",
    pattern: "admin/about/details/{id}",
    defaults: new { Controller = "About", Action = "Details" }
    );
app.MapAreaControllerRoute(

    name: "areas",
    areaName: "admin",
    pattern: "admin/about",
    defaults: new { Controller = "About", Action = "Index" }
    );
#endregion
#region Product
app.MapAreaControllerRoute(
    name: "areas",
    areaName: "admin",
    pattern: "admin/product/update/{id}",
    defaults: new { Controller = "Product", Action = "Update" }
    );
app.MapAreaControllerRoute(

    name: "areas",
    areaName: "admin",
    pattern: "admin/product/create",
    defaults: new { Controller = "Product", Action = "Create" }
    );
app.MapAreaControllerRoute(

    name: "areas",
    areaName: "admin",
    pattern: "admin/product",
    defaults: new { Controller = "Product", Action = "Index" }
    );

#endregion
#region BrandModel
app.MapAreaControllerRoute(

    name: "areas",
    areaName: "admin",
    pattern: "admin/brand/update/{id}",
    defaults: new { Controller = "BrandModel", Action = "Update" }
    );
app.MapAreaControllerRoute(

    name: "areas",
    areaName: "admin",
    pattern: "admin/brand/create",
    defaults: new { Controller = "BrandModel", Action = "Create" }
    );
app.MapAreaControllerRoute(

    name: "areas",
    areaName: "admin",
    pattern: "admin/brand/details/{id}",
    defaults: new { Controller = "BrandModel", Action = "Details" }
    );
app.MapAreaControllerRoute(

    name: "areas",
    areaName: "admin",
    pattern: "admin/brand",
    defaults: new { Controller = "BrandModel", Action = "Index" }
    );
#endregion
#region Header
app.MapAreaControllerRoute(

    name: "areas",
    areaName: "admin",
    pattern: "admin/header/update/{id}",
    defaults: new { Controller = "Header", Action = "Update" }
    );
app.MapAreaControllerRoute(

    name: "areas",
    areaName: "admin",
    pattern: "admin/header/create",
    defaults: new { Controller = "Header", Action = "Create" }
    );
app.MapAreaControllerRoute(

    name: "areas",
    areaName: "admin",
    pattern: "admin/header/details/{id}",
    defaults: new { Controller = "Header", Action = "Details" }
    );
app.MapAreaControllerRoute(

    name: "areas",
    areaName: "admin",
    pattern: "admin/header",
    defaults: new { Controller = "Header", Action = "Index" }
    );
#endregion
#region Contact
app.MapAreaControllerRoute(

    name: "areas",
    areaName: "admin",
    pattern: "admin/contact/update/{id}",
    defaults: new { Controller = "Contact", Action = "Update" }
    );
app.MapAreaControllerRoute(

    name: "areas",
    areaName: "admin",
    pattern: "admin/contact/create",
    defaults: new { Controller = "Contact", Action = "Create" }
    );
app.MapAreaControllerRoute(

    name: "areas",
    areaName: "admin",
    pattern: "admin/contact/details/{id}",
    defaults: new { Controller = "Contact", Action = "Details" }
    );
app.MapAreaControllerRoute(

    name: "areas",
    areaName: "admin",
    pattern: "admin/contact",
    defaults: new { Controller = "Contact", Action = "Index" }
    );
#endregion
#region Category 
app.MapAreaControllerRoute(

    name: "areas",
    areaName: "admin",
    pattern: "admin/category/update/{id}",
    defaults: new { Controller = "Category", Action = "Update" }
    );
app.MapAreaControllerRoute(

    name: "areas",
    areaName: "admin",
    pattern: "admin/category/create",
    defaults: new { Controller = "Category", Action = "Create" }
    );
app.MapAreaControllerRoute(

    name: "areas",
    areaName: "admin",
    pattern: "admin/category/details/{id}",
    defaults: new { Controller = "Category", Action = "Details" }
    );
app.MapAreaControllerRoute(

    name: "areas",
    areaName: "admin",
    pattern: "admin/contactInfo",
    defaults: new { Controller = "ContactInfo", Action = "Index" }
    );
#endregion

#region DashBoard 
app.MapAreaControllerRoute(

    name: "areas",
    areaName: "admin",
    pattern: "admin",
    defaults: new { Controller = "Dashboard", Action = "Index" }

    );
#endregion

#endregion

#region Ui Controller
#region Store 
app.MapControllerRoute(
    name: "store",
    pattern: "store",
    defaults: new {Controller="Stock", Action="Index"}
    );

#endregion

#region Account 
app.MapControllerRoute(
    name: "register",
    pattern: "register",
    defaults: new { Controller = "Account", Action = "Registration" }
    );
app.MapControllerRoute(
    name: "login",
    pattern: "login",
    defaults: new { Controller = "Account", Action = "Login" }
    );
#endregion

#region Home
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{Action=Index}/{id?}"
    );
#endregion
#endregion



var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
using (var scope = scopeFactory.CreateScope())
{
    var userManager = scope.ServiceProvider.GetService<UserManager<AppUser>>();
    var roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
    await DbInitializer.SeedAsync(userManager, roleManager);
    
}


app.Run();