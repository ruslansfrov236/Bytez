using bytez.business.Dto.Product;
using bytez.business.ViewModels.ProductVM;
using bytez.business.ViewModels.StockVM;
using bytez.entity.Entities;

namespace bytez.business.Abstract
{

    public interface IProductService
    {
        Task<List<Product>> GetProductsAsync();
        Task<Product> GetByIdAsync(string id);
        Task<List<Product>> GetWhereProduct(StockIndexVM model );

      
        Task<bool> Create(ProductCreateVM model);
        Task<bool> Update(ProductUpdateVM model);
        Task<bool> Delete(string id);

        Task<List<Product>> FilterRecomneyeProduct();

    }
}
