using bytez.entity.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using bytez.business.Dto.ProductImage;

namespace bytez.business.Abstract
{
    public interface IProductImageService
    {
        Task<List<ProductImage>> GetProductImages();
        Task<ProductImage> GetProductImagesById(string id);
        Task<string> UploadAsync(IFormFile file);
        bool IsImage(IFormFile file);
        bool CheckSize(IFormFile file, int maxSize);
        void Delete(string path);
        Task ShowCaseImage(string id, ShowCaseDto model);
    }
}
