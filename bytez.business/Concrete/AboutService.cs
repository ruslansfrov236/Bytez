using bytez.business.Abstract;
using bytez.business.Dto.About;
using bytez.data.Abstract;
using bytez.entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace bytez.business.Concrete
{
    public class AboutService : IAboutService
    {
        readonly private IAboutReadRepository _aboutReadRepository;
        readonly private IAboutWriteRepository _aboutWriteRepository;
        readonly private IProductImageService _productImageService;

        public AboutService(IAboutReadRepository aboutReadRepository, IAboutWriteRepository aboutWriteRepository, IProductImageService productImageService)
        {
            _aboutReadRepository = aboutReadRepository;
            _aboutWriteRepository = aboutWriteRepository;
            _productImageService = productImageService;
        }
        public async Task<bool> Create(CreateAboutDto model)
        {
            _productImageService.CheckSize(model.file, 250);
            _productImageService.IsImage(model.file);
            var newFile = await _productImageService.UploadAsync(model.file);

            About about = new About()
            {

                Description = model.Description,
                FilePath = newFile,

            };

            await _aboutWriteRepository.AddAsync(about);
            await _aboutWriteRepository.SaveAsync();
            return true;
        }

        public async Task<bool> Delete(string id)
        {
            var about = await _aboutReadRepository.GetByIdAsync(id);
            var extension = "\\wwwroot\\ui\\assets\\img\\";
            if (about != null)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), extension, about.FilePath);

                _productImageService.Delete(path);
                _aboutWriteRepository.Remove(about);
                await _aboutWriteRepository.SaveAsync();

            }
            return true;
        }

        public async Task<List<About>> GetAboutAsync()
        {
            var about = await _aboutReadRepository.GetAll().ToListAsync();

            return about;
        }

        public async Task<About> GetAboutByIdAsync(string id)
       => await _aboutReadRepository.GetByIdAsync(id);

        public async Task<bool> Update(UpdateAboutDto model)
        {
            var about = await _aboutReadRepository.GetByIdAsync(model.id);

         
         
            var extension = "\\wwwroot\\ui\\assets\\img\\";
            var path = Path.Combine(Directory.GetCurrentDirectory(), extension, about.FilePath);
            _productImageService.Delete(path);
            if (model.file != null)
            {
                _productImageService.CheckSize(model.file, 250);
                _productImageService.IsImage(model.file);
                var newFile = await _productImageService.UploadAsync(model.file);
                about.FilePath = newFile;
            }
            about.Description = model.Description;
            _aboutWriteRepository.Update(about);
            await _aboutWriteRepository.SaveAsync();

            return true;
        }
    }
}
