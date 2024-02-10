

using bytez.business.Abstract;
using bytez.business.Dto.Blog;
using bytez.data.Abstract;
using bytez.entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace bytez.business.Concrete
{
    public class BlogService : IBlogService
    {
        readonly private IVideoService _videoService;
   
        readonly private IBlogReadRepository _blogReadRepository;
        readonly private IBlogWriteRepository _blogWriteRepository;
        readonly private IProductImageService _productImageService;

        public BlogService(IBlogReadRepository blogReadRepository, IBlogWriteRepository blogWriteRepository, IProductImageService productImageService , IVideoService videoService )
        {
            _blogReadRepository = blogReadRepository;
            _blogWriteRepository = blogWriteRepository;
            _productImageService = productImageService;
            _videoService = videoService;
         
        }
        public async Task<bool> Create(CreateBlogDto model)
        {
            if (!model.isVideo)
            {
             
                _productImageService.IsImage(model.FormFile);
                _productImageService.CheckSize(model.FormFile, 250);
                var newFile = await _productImageService.UploadAsync(model.FormFile);

    
                Blog blog = new Blog()
                {
                    Title = model.Title,
                    ContentInformation = model.ContentInformation,
                    Description= model.Description  ,
                    isVideo = model.isVideo,
                    FilePath = newFile
                };
                await _blogWriteRepository.AddAsync(blog);
                await _blogWriteRepository.SaveAsync();
            }
            else
            {
              
            
                _videoService.IsVideo(model.Video);
                _videoService.CheckSize(model.Video, 250);
                var newVideo = await _videoService.UploadVideoAsync(model.Video);

              
    
                Blog blog = new Blog()
                {
                    Title = model.Title,
                    ContentInformation = model.ContentInformation,
                    Description = model.Description,
                    VideoPath = newVideo,
                    isVideo=model.isVideo
                };
                await _blogWriteRepository.AddAsync(blog);
                await _blogWriteRepository.SaveAsync();
            }

         
            

            return true;
        }

        public async Task<bool> Delete(string id)
        {
            var blog = await _blogReadRepository.GetByIdAsync(id);
            var extention = "\\wwwroot\\ui\\assets\\image\\";
            var extention2 = "\\wwwroot\\ui\\assets\\video\\";
            var path = Path.Combine(Directory.GetCurrentDirectory(), extention, blog.FilePath);
            var path2 = Path.Combine(Directory.GetCurrentDirectory(), extention2, blog.VideoPath);

            _productImageService.Delete(path);
            _videoService.Delete(path2);
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
            if (!blog.isVideo)
            {
                var extention = "\\wwwroot\\ui\\assets\\image\\";
                var path = Path.Combine(Directory.GetCurrentDirectory(), extention, blog.FilePath);
                _productImageService.Delete(path);
            }
            else
            {
                var extention2 = "\\wwwroot\\ui\\assets\\vide\\";

                var path2 = Path.Combine(Directory.GetCurrentDirectory(), extention2, blog.VideoPath);


                _videoService.Delete(path2);
            }
       
         
            if (model.FormFile != null)
            {
                _productImageService.IsImage(model.FormFile);
                _productImageService.CheckSize(model.FormFile ,250);

                var newFile = await _productImageService.UploadAsync(model.FormFile);

                blog.FilePath = newFile;
            }
            if (model.Video != null)
            {
                _videoService.IsVideo(model.Video);
                _videoService.CheckSize(model.Video, 250);
                var newVideo = await _videoService.UploadVideoAsync(model.Video);
                blog.VideoPath = newVideo;
            }
            blog.isVideo = model.isVideo;
            blog.ContentInformation = model.ContentInformation ;
            blog.Title = model.Title;
            blog.Description = model.Description ;
            _blogWriteRepository.Update(blog);
            await _blogWriteRepository.SaveAsync();
            return true;

        }
    }
}
