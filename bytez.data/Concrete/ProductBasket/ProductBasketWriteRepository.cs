using bytez.data.Abstract;
using bytez.data.Context;
using bytez.entity.Entities;

namespace bytez.data.Concrete

{
    public  class ProductBasketWriteRepository : WriteRepository<ProductBasket>, IProductBasketWriteRepository
    {
        public ProductBasketWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
