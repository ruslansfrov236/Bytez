using bytez.data.Abstract;
using bytez.data.Context;
using bytez.entity.Entities;

namespace bytez.data.Concrete
{
    public  class ProductColorReadRepository : ReadRepository<ProductColor>, IProductColorReadRepository
    {
        public ProductColorReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
