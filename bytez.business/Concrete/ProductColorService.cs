using bytez.business.Abstract;
using bytez.business.Dto.ProducColor;
using bytez.data.Abstract;
using bytez.entity.Entities;
using Microsoft.EntityFrameworkCore;

public class ProductColorService : IProductColorService
{
    readonly private IProductColorReadRepository _productColorReadRepository;
    readonly private IProductColorWriteRepository _productColorWriteRepository;

    public ProductColorService(IProductColorReadRepository productColorReadRepository, IProductColorWriteRepository productColorWriteRepository)
    {
        _productColorReadRepository = productColorReadRepository;
        _productColorWriteRepository = productColorWriteRepository;
    }

    public async Task<bool> Create(CreateProductColorDto model)
    {
        var productColor = new ProductColor { ColorName = model.ColorName };
        await _productColorWriteRepository.AddAsync(productColor);
        await _productColorWriteRepository.SaveAsync();
        return true;
    }

    public async Task<bool> Delete(string id)
    {
        var productColor = await _productColorReadRepository.GetByIdAsync(id);

        if (productColor != null)
        {
            _productColorWriteRepository.Remove(productColor);
            await _productColorWriteRepository.SaveAsync();
            return true;
        }

        return false; 
    }

    public async Task<List<ProductColor>> GetProductColorsAsync()
    {
        return await _productColorReadRepository.GetAll().ToListAsync();
    }

    public async Task<ProductColor> GetProductColorByIdAsync(string id)
    {
        return await _productColorReadRepository.GetByIdAsync(id);
    }

    public async Task<bool> Update(UpdateProductColorDto model)
    {
        var productColor = await _productColorReadRepository.GetByIdAsync(model.id);

        if (productColor != null)
        {
            productColor.ColorName = model.ColorName;
            _productColorWriteRepository.Update(productColor);
            await _productColorWriteRepository.SaveAsync();
            return true;
        }

        return false; 
    }
}
