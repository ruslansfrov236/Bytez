﻿using bytez.business.Abstract;
using bytez.data.Abstract;
using bytez.entity.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;

namespace bytez.business.Concrete
{
    public class ProductImageService : IProductImageService
    {
        readonly private IProductImageReadRepository _productImageReadRepository;
        readonly private IProductImageWriteRepository _productImageWriteRepository;

        public ProductImageService(IProductImageReadRepository productImageReadRepository , IProductImageWriteRepository productImageWriteRepository  )
        {
           
            _productImageReadRepository = productImageReadRepository;
            _productImageWriteRepository = productImageWriteRepository;
           


        }
        public bool CheckSize(IFormFile file, int maxSize)
        {
            if (file.Length / 1024 < maxSize)
            {

               
                return false;
            }
            return true;
        }

        public void Delete(string path)
        {
            if (File.Exists(path))
                File.Delete(path);
        }

        public async Task<List<ProductImage>> GetProductImages()
        {
            var image = await _productImageReadRepository.GetAll().ToListAsync();

            return image;
        }

        public async Task<ProductImage> GetProductImagesById(string id)
        => await _productImageReadRepository.GetByIdAsync(id);

        public bool IsImage(IFormFile file) 
        {
            if (file == null || file.Length == 0)
            {

             
                return false;

            }


            if (!file.ContentType.StartsWith("image/"))
            {
            
                return false;
            }


            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!allowedExtensions.Contains(fileExtension))
            {
              
                return false;
            }

            return true;
        }

     

        public async Task<string> UploadAsync(IFormFile file)
        {
            var filename = $"{Guid.NewGuid()}_{file.FileName}";

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ui/assets/image/", filename);
            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ui/assets/image/", filename);
            var pngPath = Path.ChangeExtension(imagePath, "png");
            using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
            {
                await file.CopyToAsync(fileStream);
            }
            using (var image = Image.Load(imagePath))
            {
                image.Save(pngPath, new PngEncoder());
            }

       
            File.Delete(imagePath);

            return Path.GetFileName(pngPath);
           
        }
    }
}
