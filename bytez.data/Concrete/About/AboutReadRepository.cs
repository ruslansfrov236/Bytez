using bytez.data.Abstract;
using bytez.data.Context;
using bytez.entity.Entities;

namespace bytez.data.Concrete
{
    public  class AboutReadRepository : ReadRepository<About>, IAboutReadRepository
    {
        public AboutReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
