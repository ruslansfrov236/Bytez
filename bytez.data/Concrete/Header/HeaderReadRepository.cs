using bytez.data.Abstract;
using bytez.data.Context;
using bytez.entity.Entities;

namespace bytez.data.Concrete
{
    public class HeaderReadRepository : ReadRepository<Header>, IHeaderReadRepository
    {
        public HeaderReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
