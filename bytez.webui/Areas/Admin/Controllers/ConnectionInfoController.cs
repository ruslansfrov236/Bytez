using bytez.business.Abstract;
using bytez.business.Dto.ConnectionInfo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bytez.webui.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class ConnectionInfoController : Controller
    {
        readonly private IConnectionInfoService _connectionInfoService;

        public ConnectionInfoController(IConnectionInfoService connectionInfoService)
        {
            _connectionInfoService = connectionInfoService;
        }
        [Authorize(Roles = "Admin , Manager")]
        public async Task<IActionResult> Index()
        {
            var connectionInfos = await _connectionInfoService.GetAllConnections();

            return View(connectionInfos);
        }
        [Authorize(Roles = "Admin , Manager")]
        public async Task<IActionResult> Details(string id)
        {
            var connectionInfo = await _connectionInfoService.GetConnectionInfoById(id);
            if (connectionInfo == null) return NotFound();


            return View(connectionInfo);
        }
        [Authorize(Roles = "Admin ")]
        public async Task<IActionResult> Create()
        => View();
        [HttpPost]
        [Authorize(Roles = "Admin ")]
        public async Task<IActionResult> Create(CreateConnectionInfoDto model)
        {
            await _connectionInfoService.Create(model);

            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Admin ")]
        public async Task<IActionResult> Update(string id)
        {
            var connectionInfo = await _connectionInfoService.GetConnectionInfoById(id);

            if (connectionInfo == null) return NotFound();
            UpdateConnectionInfoDto updateConnectionInfoDto = new UpdateConnectionInfoDto()
            {
                Title = connectionInfo.Title,
                Description= connectionInfo.Description,
                FilePath= connectionInfo.FilePath,
                File=connectionInfo.File

            };
            return View(updateConnectionInfoDto);

        }
        [HttpPost]
        [Authorize(Roles = "Admin ")]
        public async Task<IActionResult> Update(UpdateConnectionInfoDto model)
        {
            
            await _connectionInfoService.Update(model);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            var connectionInfo = await _connectionInfoService.GetConnectionInfoById(id);
            if (connectionInfo == null) return BadRequest();

           await _connectionInfoService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
