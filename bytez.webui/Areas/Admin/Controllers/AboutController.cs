using bytez.business.Abstract;
using bytez.business.Dto.About;
using bytez.entity.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bytez.webui.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Authorize(Roles = nameof(Admin))]
    public class AboutController : Controller
    {

        readonly private IAboutService _aboutService;
        public AboutController(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }
        public async Task<IActionResult> Index()
        {
            List<About> aboutList = await _aboutService.GetAboutAsync();
            return View(aboutList);
        }
        public async Task<IActionResult> Details(string id)
        {
            var about = await _aboutService.GetAboutByIdAsync(id);
            if (about == null) return NotFound();
            return View(about);
        }
        public async Task<IActionResult> Create()
        {
            var about =await _aboutService.GetAboutAsync();
            if(about==null) return View();
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateAboutDto model)
        {

            if(!ModelState.IsValid) return NotFound();
                await _aboutService.Create(model);

                return RedirectToAction(nameof(Index));
            
        }

        public async Task<IActionResult> Update(string id)
        {
            var about = await _aboutService.GetAboutByIdAsync(id);
            if (about == null) return NotFound();
              UpdateAboutDto updateAbout=  new UpdateAboutDto(){
                  FilePath= about.FilePath,
                  Description=about.Description,
                  file=about.file
                

              };
            return View(updateAbout);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateAboutDto model)
        {
            await _aboutService.Update(model);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var about = await _aboutService.GetAboutByIdAsync(id);
            if (about == null) return NotFound();
            await _aboutService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
