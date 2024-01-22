using bytez.entity.Entities;
using bytez.data.Abstract;
using bytez.data.Context;

namespace bytez.data.Concrete
{
    public  class CategoryReadRepository : ReadRepository<Category>, ICategoryReadRepository
    {
        public CategoryReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
