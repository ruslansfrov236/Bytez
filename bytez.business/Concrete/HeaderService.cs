using bytez.business.Abstract;
using bytez.business.Dto.Header;
using bytez.data.Abstract;
using bytez.entity.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bytez.business.Concrete
{
    public class HeaderService : IHeaderService
    {
        readonly private IHeaderReadRepository _headerReadRepository;
        readonly private IHeaderWriteRepository _headerWriteRepository;
        readonly private IProductImageService _productImageService;

        public HeaderService(IHeaderReadRepository headerReadRepository, IProductImageService productImageService, IHeaderWriteRepository headerWriteRepository)
        {
            _headerReadRepository = headerReadRepository;
            _headerWriteRepository = headerWriteRepository;
            _productImageService = productImageService;
        }
        public async Task<bool> Create(CreateHeaderDto model)
        {
            _productImageService.IsImage(model.File);

            _productImageService.CheckSize(model.File, 250);

            var newFile = await _productImageService.UploadAsync(model.File);

            await _headerWriteRepository.AddAsync(new Header() { Title = model.Title, Descripton = model.Descripton, FilePath = newFile });

            await _headerWriteRepository.SaveAsync();

            return true;
        }

        public async Task<bool> Delete(string id)
        {
            var header = await _headerReadRepository.GetByIdAsync(id);
            var extention = "\\wwwroot\\assets\\image\\";
            var path = Path.Combine(Directory.GetCurrentDirectory(), extention, header.FilePath);
            _productImageService.Delete(path);
            await _headerWriteRepository.RemoveAsync(id);
            await _headerWriteRepository.SaveAsync();

            return true;
        }

        public async Task<Header> GetHeaderByIdAsync(string id)
        => await _headerReadRepository.GetByIdAsync(id);

        public async Task<List<Header>> GetHeaderListAsync()
        {
            List<Header> headers = await _headerReadRepository.GetAll().ToListAsync();

            return headers;
        }

        public async Task<bool> UpdateAsync(UpdateHeaderDto model)
        {
            var header = await _headerReadRepository.GetByIdAsync(model.Id);
            var extention = "\\wwwroot\\assets\\image\\";
            var path = Path.Combine(Directory.GetCurrentDirectory(), extention, header.FilePath);
            _productImageService.Delete(path);

            if (model.File != null)
            {
                _productImageService.IsImage(model.File);

                _productImageService.CheckSize(model.File, 250);

                var newFile = await _productImageService.UploadAsync(model.File);
                header.FilePath = newFile;
            }
            header.Descripton = model.Description;
            header.Title = model.Title;


            _headerWriteRepository.Update(header);
            await _headerWriteRepository.SaveAsync();

            return true;
        }
    }
}
