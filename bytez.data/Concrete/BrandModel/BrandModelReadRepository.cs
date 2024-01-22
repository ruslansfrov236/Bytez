using bytez.data.Abstract;
using bytez.data.Context;
using bytez.entity.Entities;

namespace bytez.data.Concrete
{
    public  class BrandModelReadRepository : ReadRepository<BrandModel> , IBrandModelReadRepository
    {
        public BrandModelReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
