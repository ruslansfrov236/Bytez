using bytez.business.Abstract;
using bytez.business.Dto.Blog;
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
    public class BlogService : IBlogService
    {
        readonly private IBlogReadRepository _blogReadRepository;
        readonly private IBlogWriteRepository _blogWriteRepository;
        readonly private IProductImageService _productImageService;

        public BlogService(IBlogReadRepository blogReadRepository, IBlogWriteRepository blogWriteRepository, IProductImageService productImageService)
        {
            _blogReadRepository = blogReadRepository;
            _blogWriteRepository = blogWriteRepository;
            _productImageService = productImageService;
        }
        public async Task<bool> Create(CreateBlogDto model)
        {
            _productImageService.IsImage(model.File);
            _productImageService.CheckSize(model.File, 250);
            var newFile = await _productImageService.UploadAsync(model.File);

            Blog blog = new Blog()
            {
                Title = model.Title,
                ContentInformation = model.ContentInformation,
                Description = model.Description,
                FilePath = newFile
            };
            await _blogWriteRepository.AddAsync(blog);
            await _blogWriteRepository.SaveAsync();
            return true;
        }

        public async Task<bool> Delete(string id)
        {
            var blog = await _blogReadRepository.GetByIdAsync(id);
            var extention = "\\wwwroot\\ui\\assets\\image\\";
            var path = Path.Combine(Directory.GetCurrentDirectory(), extention, blog.FilePath);

            _productImageService.Delete(path);

            _blogWriteRepository.Remove(blog);
            await _blogWriteRepository.SaveAsync();

            return true;
        }

        public Task<Blog> GetBlogById(string id)
        => _blogReadRepository.GetByIdAsync(id);

        public async Task<List<Blog>> GetBlogListAsync()
        {
            List<Blog> blogs = await _blogReadRepository.GetAll().ToListAsync();
            if (blogs == null) return default;
            return blogs;
        }

        public async Task<bool> Update(UpdateBlogDto model)
        {
            var blog = await _blogReadRepository.GetByIdAsync(model.id);
            var extention = "\\wwwroot\\ui\\assets\\image\\";
            var path = Path.Combine(Directory.GetCurrentDirectory(), extention, blog.FilePath);

            _productImageService.Delete(path);

            if (model.File != null)
            {
                _productImageService.IsImage(model.File);
                _productImageService.CheckSize(model.File ,250);

                var newFile = await _productImageService.UploadAsync(model.File);

                blog.FilePath = newFile;
            }
            blog.ContentInformation = model.ContentInformation;
            blog.Title = model.Title;
            blog.Description = model.Description;
            _blogWriteRepository.Update(blog);
            await _blogWriteRepository.SaveAsync();
            return true;

        }
    }
}
