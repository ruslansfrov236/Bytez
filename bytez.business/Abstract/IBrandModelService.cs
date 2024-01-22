using bytez.business.Dto.BrandModel;
using bytez.entity.Entities;

namespace bytez.business.Abstract
{
    public interface IBrandModelService
    {
        Task<List<BrandModel>> GetBrandsAsync(); 
        Task<BrandModel> GetBrandByIdAsync(string id);

        Task<bool> Create(CreateBrandModelDto model);
        Task<bool> Update(UpdateBrandModelDto model);
        Task<bool> Delete( string id );
    }
}
