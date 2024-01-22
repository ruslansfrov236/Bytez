using bytez.data.Abstract;
using bytez.data.Context;
using bytez.entity.Entities;

namespace bytez.data.Concrete
{
    public  class ProductImageReadRepository : ReadRepository<ProductImage>, IProductImageReadRepository
    {
        public ProductImageReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
