using bytez.entity.Entities.Customer;

namespace bytez.data.Abstract
{
    public interface IWriteRepository<T> where T : BaseEntity 
   
    {
        Task<bool> AddAsync(T model);
        Task<bool> AddRangeAsync(List<T> data);
        bool Remove(T model);
        bool RemoveRange(List<T> data);
        Task<bool> RemoveAsync(string id);
        bool Update(T model);

        Task<int> SaveAsync();
    }
}
