using bytez.business.Dto.About;
using bytez.entity.Entities;

namespace bytez.business.Abstract
{
    public interface IAboutService
    {
        Task<List<About>> GetAboutAsync();
        Task<About> GetAboutByIdAsync(string id);
        Task<bool> Create(CreateAboutDto model);
        Task<bool> Update(UpdateAboutDto model);

        Task<bool> Delete(string id);
    }
}
