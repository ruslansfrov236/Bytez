

using bytez.data.Abstract;
using bytez.data.Context;
using bytez.entity.Entities;

namespace bytez.data.Concrete
{
    public  class ProductColorWriteRepository : WriteRepository<ProductColor>, IProductColorWriteRepository
    {
        public ProductColorWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
