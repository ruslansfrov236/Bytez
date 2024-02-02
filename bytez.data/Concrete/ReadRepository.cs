using bytez.data.Abstract;
using bytez.data.Context;
using bytez.entity.Entities.Customer;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace bytez.data.Concrete
{
    public abstract class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        readonly private AppDbContext _context;

        public ReadRepository(AppDbContext context)
        {
            _context = context;
        }
        private DbSet<T> Table => _context.Set<T>();
        public IQueryable<T> GetAll(bool tracking = true)
        {
            var queryable = Table.AsQueryable();
            if (!tracking)
                queryable = Table.AsNoTracking();

            return queryable;
        }

        public async Task<T> GetByIdAsync(string id, bool tracking = true)
        {
            var queryable = Table.AsQueryable();
            if (!tracking)
                queryable = Table.AsNoTracking();
            return await queryable.FirstOrDefaultAsync(a => a.Id == Guid.Parse(id));
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true)
        {
            var queryable = Table.AsQueryable();
            if (!tracking)
                queryable = Table.AsNoTracking();
            return await queryable.FirstOrDefaultAsync(method);
        }

        public IQueryable<T> GetWhere(  Expression<Func<T, bool>> method, bool tracking = true)
        {
            var queryable = Table.AsQueryable();
            if (!tracking)
                queryable = Table.AsNoTracking();
            return queryable.Where(method) ;
        }
    }
}
