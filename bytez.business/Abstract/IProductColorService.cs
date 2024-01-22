using bytez.business.Dto.ProducColor;
using bytez.entity.Entities;

namespace bytez.business.Abstract
{
    public interface IProductColorService
    {
        Task<List<ProductColor>> GetProductColorsAsync();
        Task<ProductColor> GetProductColorByIdAsync(string id);

        Task<bool> Create(CreateProductColorDto model);
        Task<bool> Update(UpdateProductColorDto model);
        Task<bool> Delete(string id);
    }
}
