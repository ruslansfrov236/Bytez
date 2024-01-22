using bytez.business.Abstract;
using bytez.business.Dto.BrandModel;
using bytez.data.Abstract;
using bytez.data.Concrete;
using bytez.entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace bytez.business.Concrete
{
    public class BrandModelService : IBrandModelService
    {
        readonly private IBrandModelReadRepository _brandModelReadRepository;

        readonly private IBrandModelWriteRepository _brandModelWriteRepository;
        readonly IProductImageService _productImageService;
        public BrandModelService(IBrandModelReadRepository brandModelReadRepository, IBrandModelWriteRepository brandModelWriteRepository, IProductImageService productImageService)
        {
            _brandModelReadRepository = brandModelReadRepository;
            _brandModelWriteRepository = brandModelWriteRepository;
            _productImageService = productImageService;
        }
        public async Task<bool> Create(CreateBrandModelDto model)
        {
            _productImageService.CheckSize(model.formFile, 250);
            _productImageService.IsImage(model.formFile);
            var newFile = await _productImageService.UploadAsync(model.formFile);
            BrandModel brandModel = new BrandModel()
            {
                Name = model.Name,
                FilePath = newFile
            };
            await _brandModelWriteRepository.AddAsync(brandModel);
            await _brandModelWriteRepository.SaveAsync();
            return true;

        }

        public async Task<bool> Delete(string id)
        {
            var brandModel = await _brandModelReadRepository.GetByIdAsync(id);
            var path = Path.Combine(Directory.GetCurrentDirectory(), "\\wwwroot\\ui\\assets\\image\\", brandModel.FilePath);
             _productImageService.Delete(path);
            await _brandModelWriteRepository.RemoveAsync(id);
            await _brandModelWriteRepository.SaveAsync();
            return true;
        }

        public async Task<BrandModel> GetBrandByIdAsync(string id)
        => await _brandModelReadRepository.GetByIdAsync(id);

        public async Task<List<BrandModel>> GetBrandsAsync()
        {
            List<BrandModel> brandModels = await _brandModelReadRepository.GetAll().ToListAsync();

            return brandModels;
        }

        public async Task<bool> Update(UpdateBrandModelDto model)
        {
            var brandsModel = await _brandModelReadRepository.GetByIdAsync(model.id);
            var path = Path.Combine(Directory.GetCurrentDirectory(), "\\wwwroot\\ui\\assets\\image\\", brandsModel.FilePath);
           
            _productImageService.Delete(path);
            if(model.formFile!=null){
                _productImageService.CheckSize(model.formFile, 250);
            _productImageService.IsImage(model.formFile);
            var newFile = await _productImageService.UploadAsync(model.formFile);
             brandsModel.FilePath=newFile;
            }

            brandsModel.Name = model.Name;
           
            _brandModelWriteRepository.Update(brandsModel);
            await _brandModelWriteRepository.SaveAsync();
            return true;

        }
    }
}
