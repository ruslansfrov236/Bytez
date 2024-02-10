using Microsoft.AspNetCore.Http;

namespace bytez.business.Abstract
{
    public interface IVideoService
    {
        
        Task<string> UploadVideoAsync(IFormFile file);
        bool IsVideo(IFormFile file);
        bool CheckSize(IFormFile file, int maxSize);
        void Delete(string path);
    }
}
