using bytez.business.Abstract;
using bytez.business.Dto.Product;
using bytez.business.ViewModels.ProductVM;
using bytez.business.ViewModels.StockVM;
using bytez.data.Abstract;
using bytez.data.Context;
using bytez.entity.Entities;
using bytez.entity.Entities.Enum;

using Microsoft.EntityFrameworkCore;

namespace bytez.business.Concrete
{
    public class ProductService : IProductService
    {

        readonly private IProductReadRepository _productReadRepository;

        readonly private ICategoryService _categoryService;
        readonly private IProductWriteRepository _productWriteRepository;
        readonly private IProductImageService _productImageService;
        readonly private IProductImageReadRepository _productImageReadRepository;
        readonly private IProductImageWriteRepository _productImageWriteRepository;
        readonly private AppDbContext _context;
        public ProductService(IProductReadRepository productReadRepository
                            , IProductWriteRepository productWriteRepository,
                              IProductImageService productImageService,
                              IProductImageWriteRepository productImageWriteRepository,
                              IProductImageReadRepository productImageReadRepository,
                              ICategoryService categoryService)
        {
            _productImageService = productImageService;
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
            _productImageWriteRepository = productImageWriteRepository;
            _productImageReadRepository = productImageReadRepository;
            _categoryService=categoryService;

        }
        public async Task<bool> Create(CreateProductDto model)
        {
            var uplaodFile = await _productImageReadRepository.GetAll()
                                                                .Include(p => p.Products)
                                                               .ToListAsync();

            _productImageService.IsImage(model.ProductFile);
            _productImageService.CheckSize(model.ProductFile, 250);
            var newProductImage = await _productImageService.UploadAsync(model.ProductFile);

            Product product = new Product()
            {

                Title = model.Title,
               
                BrandsId = model.BrandsId,
                CategoryId = model.CategoryId,
                Discount = model.Discount,
                
                ProductMemory = model.ProductMemory,
                ProductRam = model.ProductRam,
                ProfileProduct = newProductImage,
                Higlist = model.Higlist,
                ColorId = model.ColorId,
                Description = model.Description,
                Stock = model.Stock,
                FilePath = uplaodFile.FirstOrDefault()?.FilePath,
                Price = model.Price
            };

            await _productWriteRepository.AddAsync(product);

            if (model.Images != null)
            {
                foreach (var files in model.Images)
                {
                    _productImageService.CheckSize(files, 250);
                    _productImageService.IsImage(files);
                    var newFile = await _productImageService.UploadAsync(files);




                    await _productImageWriteRepository.AddAsync(new ProductImage
                    {
                        FilePath = newFile,
                        ProductsId = product.Id

                    });

                }

            }


            await _productWriteRepository.SaveAsync();
            return true;

        }

        public async Task<bool> Delete(string id)
        {
            var product = await _productReadRepository.GetByIdAsync(id);
            var extension = "\\wwwroot\\ui\\assets\\image\\";
            if (product != null)

                try
                {

                    var path = Path.Combine(Directory.GetCurrentDirectory(), extension, product.FilePath);
                    var path2 = Path.Combine(Directory.GetCurrentDirectory(), extension, product.ProfileProduct);

                    _productImageService.Delete(path);
                    _productImageService.Delete(path2);

                    _productWriteRepository.Remove(product);
                    await _productWriteRepository.SaveAsync();

                }
                catch (Exception ex)
                {


                }
            return true;
        }

        public async Task<List<Product>> FilterRecomneyeProduct()
        {
            var product = await _productReadRepository
                .GetWhere(p => (int)p.Discount == (int)Discount.FiftyPercentOff)
                .ToListAsync();



            return product;
        }




        public async Task<Product> GetByIdAsync(string id)
         => await _productReadRepository.GetByIdAsync(id);



        public async Task<List<Product>> GetProductsAsync()
        {
            var products = await _productReadRepository.GetAll()
                 .Include(t => t.Category)
                .Include(p => p.Color)
                .Include(a => a.Brands)
               
                  .Include(p => p.Wishlist)
                .ToListAsync();

            return products;
        }

        public async Task<List<Product>> GetWhereProduct(StockIndexVM model)
        {
            IQueryable<Product> query = _productReadRepository.GetAll().Include(p => p.Brands)
                                                                      
                                                                     .Include(p => p.Category)
                                                                     .Include(p => p.Color)
                                                                     .Include(p=>p.Wishlist);

            if (model.ProductWhereDto != null)
            {
               
                query = query.Where(pr => (model.ProductWhereDto.CategoryId != null ? pr.CategoryId == Guid.Parse(model.ProductWhereDto.CategoryId) : true));
                query = query.Where(pr => (model.ProductWhereDto.minValue != null ? pr.Price >= model.ProductWhereDto.minValue : true) && (model.ProductWhereDto.maxValue != null ? pr.Price <= model.ProductWhereDto.maxValue : true));
                query = query.Where(pr => (model.ProductWhereDto.BrandsId != null ? pr.BrandsId == Guid.Parse(model.ProductWhereDto.BrandsId):true));
                query = query.Where(pr => (model.ProductWhereDto.ColorId != null ? pr.ColorId==Guid.Parse(model.ProductWhereDto.ColorId):true)) ;
            }

            return await query.ToListAsync();
        }









        public async Task<bool> Update(UpdateProductDto model)
        {
            var product = await _productReadRepository.GetByIdAsync(model.id);
            var extension = "\\wwwroot\\ui\\assets\\image\\";
             
            var path2 = Path.Combine(Directory.GetCurrentDirectory(), extension, product.ProfileProduct);
            var images = _productImageReadRepository.GetAll();
            if (product != null)


                _productImageService.Delete(path2);

            if (model.ProductFile != null)
            {
                _productImageService.CheckSize(model.ProductFile, 250);
                _productImageService.IsImage(model.ProductFile);
                var newProductImage = await _productImageService.UploadAsync(model.ProductFile);
                product.ProfileProduct = newProductImage;
            }

            if (model.Images != null)
            {
                foreach (var file in model.Images)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), extension, file.Name);
                    _productImageService.Delete(path);
                    _productImageService.CheckSize(file, 250);
                    _productImageService.IsImage(file);
                    var newFile = await _productImageService.UploadAsync(file);
                    await _productImageWriteRepository.AddAsync(new ProductImage { FilePath = newFile, ProductsId = product.Id });

                }
                product.FilePath = images.FirstOrDefault()?.FilePath;
            }
            
            product.Title = model.Title;
       
            product.BrandsId = model.BrandsId;
            product.CategoryId = model.CategoryId;
            product.ColorId = model.ColorId;
            product.Discount = model.Discount;
            product.Stock = model.Stock;
            product.Price = model.Price;
            product.Description = model.Description;
            product.ProductMemory = model.ProductMemory;
            product.ProductRam = model.ProductRam;
            product.Higlist = model.Higlist;
            
            _productWriteRepository.Update(product);
            await _productWriteRepository.SaveAsync();
            return true;
        }
    }
}
