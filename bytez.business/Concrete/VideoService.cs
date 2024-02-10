using bytez.business.Abstract;
using bytez.data.Abstract;
using bytez.entity.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bytez.business.Concrete
{
    public class VideoService : IVideoService
    {
       

      
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

      
       
        public bool IsVideo(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {


                return false;

            }


            if (!file.ContentType.StartsWith("video/"))
            {

                return false;
            }


            var allowedExtensions = new[] { "mp4", ".mkv", ".mpg", ".webm" , ".wmv" };
            var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!allowedExtensions.Contains(fileExtension))
            {

                return false;
            }

            return true;
        }

        public async Task<string> UploadVideoAsync(IFormFile file)
        {
            var filename = $"{Guid.NewGuid()}_{file.FileName}";

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ui/assets/video/", filename);

            using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
            {
                await file.CopyToAsync(fileStream);
            }
            return filename;
        }
    }
}
