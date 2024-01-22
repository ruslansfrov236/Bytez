using bytez.entity.Entities;
using bytez.data.Abstract;
using bytez.data.Context;

namespace bytez.data.Concrete
{
    public  class BasketWriteRepository : WriteRepository<Basket>, IBasketWriteRepository
    {
        public BasketWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
