
using bytez.data.Abstract;
using bytez.data.Context;
using bytez.entity.Entities;

namespace bytez.data.Concrete
{
    public  class BasketReadRepository : ReadRepository<Basket>, IBasketReadRepository
    {
        public BasketReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
