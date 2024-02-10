using bytez.business.Abstract;
using bytez.business.Dto.Category;
using bytez.data.Abstract;
using bytez.entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace bytez.business.Concrete
{
    public class CategoryService : ICategoryService
    {
        readonly private ICategoryReadRepository _categoryReadRepository;
        readonly private ICategoryWriteRepository _categoryWriteRepository;

        public CategoryService(ICategoryReadRepository categoryReadRepository, ICategoryWriteRepository categoryWriteRepository)
        {
            _categoryReadRepository = categoryReadRepository;
            _categoryWriteRepository = categoryWriteRepository;
        }
        public async Task<bool> Create(CreateCategoryDto model)
        {
            Category category = new Category()
            {
                Name = model.Name,

            };

            await _categoryWriteRepository.AddAsync(category);
            await _categoryWriteRepository.SaveAsync();

            return true;

        }

        public async Task<bool> Delete(string id)
        {
            var category = await _categoryReadRepository.GetByIdAsync(id);

            _categoryWriteRepository.Remove(category);
            await _categoryWriteRepository.SaveAsync();
            return true;
        }

        public async Task<List<Category>> GetCategoryAsync()
        {

            var categories = await _categoryReadRepository.GetAll()
               .Include(p => p.Product).
                Select(a => new Category()
                                        { Id = a.Id,
                                         Name = a.Name,
                                         Count = a.Product.Where(p => p.CategoryId == a.Id).Count(), 
                                         CreatedDate = a.CreatedDate,
                                         UpdatedDate = a.UpdatedDate })
                .ToListAsync();

            return categories;
        }


        public async Task<Category> GetCategoryByIdAsync(string id)
       => await _categoryReadRepository.GetByIdAsync(id);
        public async Task<bool> Update(UpdateCategoryDto model)
        {
            var category = await _categoryReadRepository.GetByIdAsync(model.Id);

            category.Name = model.Name;

            _categoryWriteRepository.Update(category);
            await _categoryWriteRepository.SaveAsync();
            return true;
        }
    }
}
