using bytez.business.Abstract;
using bytez.entity.Entities;
using bytez.webui.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace bytez.webui.Controllers
{
    public class HomeController : Controller
    {
        readonly private IHeaderService _headerService;
        readonly private IConnectionInfoService _connectionInfoService;
        readonly private IProductService _productService;
        public HomeController(IHeaderService headerService , IConnectionInfoService connectionInfoService, IProductService productService)
        {
            _headerService = headerService;
            _connectionInfoService = connectionInfoService;
            _productService = productService;
        }

        public async Task<IActionResult> IndexAsync()
        {
            List<Header> header = await _headerService.GetHeaderListAsync();
            List<entity.Entities.ConnectionInfo> connectionInfos = await _connectionInfoService.GetAllConnections();
            List<Product> products = await _productService.FilterRecomneyeProduct();
            HomeIndexVM model = new HomeIndexVM(){
                Headers=header,
                ConnectionInfos= connectionInfos,
                Products=products

            };
            return View(model) ;
        }
    }
}
