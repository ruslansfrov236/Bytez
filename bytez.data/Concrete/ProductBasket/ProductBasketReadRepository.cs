using bytez.data.Abstract;
using bytez.data.Context;
using bytez.entity.Entities;

namespace bytez.data.Concrete
{
    public  class ProductBasketReadRepository : ReadRepository<ProductBasket>, IProductBasketReadRepository
    {
        public ProductBasketReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
