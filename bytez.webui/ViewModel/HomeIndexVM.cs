using bytez.entity.Entities;
using bytez.entity.Entities.Identity;
using c=bytez.entity.Entities;

namespace bytez.webui.ViewModel
{
    public class HomeIndexVM
    {
        public List<Header> Headers { get; set; }
        public List<c::ConnectionInfo> ConnectionInfos { get; set; }
        public List<Product> Products { get; set; }

       
    }
}
