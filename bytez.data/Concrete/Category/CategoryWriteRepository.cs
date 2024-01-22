using bytez.entity.Entities;
using bytez.data.Abstract;
using bytez.data.Context;

namespace bytez.data.Concrete
{
    public  class CategoryWriteRepository : WriteRepository<Category>, ICategoryWriteRepository
    {
        public CategoryWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
