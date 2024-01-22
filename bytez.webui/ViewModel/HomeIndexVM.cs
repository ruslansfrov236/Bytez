using bytez.entity.Entities;
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
