using bytez.business.Dto.Category;
using bytez.entity.Entities;

namespace bytez.business.Abstract
{
    public interface ICategoryService
    {
        Task<List<Category>> GetCategoryAsync();
        Task<Category> GetCategoryByIdAsync(string id);

        Task<bool> Create(CreateCategoryDto model);
        Task<bool> Update(UpdateCategoryDto model);
        Task<bool> Delete(string id);
    }
}
