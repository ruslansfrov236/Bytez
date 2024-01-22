using bytez.business.Abstract;
using bytez.business.Dto.ConnectionInfo;
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
        public async Task<IActionResult> Index()
        {
            var connectionInfos = await _connectionInfoService.GetAllConnections();

            return View(connectionInfos);
        }

        public async Task<IActionResult> Details(string id)
        {
            var connectionInfo = await _connectionInfoService.GetConnectionInfoById(id);
            if (connectionInfo == null) return NotFound();


            return View(connectionInfo);
        }
        public async Task<IActionResult> Create()
        => View();
        [HttpPost]
        public async Task<IActionResult> Create(CreateConnectionInfoDto model)
        {
            await _connectionInfoService.Create(model);

            return RedirectToAction(nameof(Index));
        }
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
        public async Task<IActionResult> Update(UpdateConnectionInfoDto model)
        {
            
            await _connectionInfoService.Update(model);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var connectionInfo = await _connectionInfoService.GetConnectionInfoById(id);
            if (connectionInfo == null) return BadRequest();

           await _connectionInfoService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
