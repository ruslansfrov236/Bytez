using bytez.business.Dto.Blog;
using bytez.entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bytez.business.Abstract
{
    public interface IBlogService
    {
        Task<List<Blog>> GetBlogListAsync();
        Task<Blog> GetBlogById (string  id);
        Task<bool> Create(CreateBlogDto model);
        Task<bool> Update(UpdateBlogDto model);
        Task<bool> Delete(string id);
    }
}
