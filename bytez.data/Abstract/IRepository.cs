using bytez.entity.Entities.Customer;
using Microsoft.EntityFrameworkCore;

namespace bytez.data.Abstract
{
    public interface IRepository< T> where T : BaseEntity
    {
        DbSet<T> Table { get;}
    }
}
