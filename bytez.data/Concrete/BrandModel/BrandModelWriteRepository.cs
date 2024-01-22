using bytez.entity.Entities;
using bytez.data.Abstract;
using bytez.data.Context;

namespace bytez.data.Concrete
{
    public  class BrandModelWriteRepository : WriteRepository<BrandModel>, IBrandModelWriteRepository
    {
        public BrandModelWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
