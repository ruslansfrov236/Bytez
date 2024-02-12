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
        readonly private IProductWriteRepository _productWriteRepository;
        readonly private IProductImageService _productImageService;
        readonly private IProductImageReadRepository _productImageReadRepository;
        readonly private IProductImageWriteRepository _productImageWriteRepository;
        readonly private AppDbContext _context;
        public ProductService(IProductReadRepository productReadRepository
                            , IProductWriteRepository productWriteRepository,
                              IProductImageService productImageService,
                              IProductImageWriteRepository productImageWriteRepository,
                              IProductImageReadRepository productImageReadRepository)
        {
            _productImageService = productImageService;
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
            _productImageWriteRepository = productImageWriteRepository;
            _productImageReadRepository = productImageReadRepository;

        }
        public async Task<bool> Create(ProductCreateVM model)
        {
            var uplaodFile = await _productImageReadRepository.GetAll()
                                                                .Include(p => p.Products)
                                                               .ToListAsync();

            _productImageService.IsImage(model.CreateProductDto.ProductFile);
            _productImageService.CheckSize(model.CreateProductDto.ProductFile, 250);
            var newProductImage = await _productImageService.UploadAsync(model.CreateProductDto.ProductFile);

            Product product = new Product()
            {

                Title = model.CreateProductDto.Title,
                Avg = model.CreateProductDto.Avg,
                BrandsId = model.CreateProductDto.BrandsId,
                CategoryId = model.CreateProductDto.CategoryId,
                Discount = model.CreateProductDto.Discount,
                ProductMemory = model.CreateProductDto.ProductMemory,
                ProductRam = model.CreateProductDto.ProductRam,
                ProfileProduct = newProductImage,
                Higlist = model.CreateProductDto.Higlist,
                ColorId = model.CreateProductDto.ColorId,
                Description = model.CreateProductDto.Description,
                Stock = model.CreateProductDto.Stock,
                FilePath = uplaodFile.FirstOrDefault()?.FilePath,
                Price = model.CreateProductDto.Price
            };

            await _productWriteRepository.AddAsync(product);
            foreach (var files in model.CreateProductDto.Images)
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
                .Include(p => p.Color)
                .Include(a => a.Brands)
                .Include(t => t.Category)
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
                query = query.Where(pr =>
                    (model.ProductWhereDto.minValue == null || pr.Price >= model.ProductWhereDto.minValue) &&
                    (model.ProductWhereDto.maxValue == null || pr.Price <= model.ProductWhereDto.maxValue) &&
                    (model.ProductWhereDto.BrandsId == null || pr.BrandsId == Guid.Parse(model.ProductWhereDto.BrandsId)) &&
                    (model.ProductWhereDto.ColorId == null || pr.ColorId == Guid.Parse(model.ProductWhereDto.ColorId)) &&
                    (model.ProductWhereDto.CategoryId == null || pr.CategoryId == Guid.Parse(model.ProductWhereDto.CategoryId))
                );
            }

            return await query.ToListAsync();
        }









        public async Task<bool> Update(ProductUpdateVM model)
        {
            var product = await _productReadRepository.GetByIdAsync(model.UpdateProductDto.id);
            var extension = "\\wwwroot\\ui\\assets\\image\\";

            var path2 = Path.Combine(Directory.GetCurrentDirectory(), extension, product.ProfileProduct);
            var images = _productImageReadRepository.GetAll();
            if (product != null)


                _productImageService.Delete(path2);

            if (model.UpdateProductDto.ProductFile != null)
            {
                _productImageService.CheckSize(model.UpdateProductDto.ProductFile, 250);
                _productImageService.IsImage(model.UpdateProductDto.ProductFile);
                var newProductImage = await _productImageService.UploadAsync(model.UpdateProductDto.ProductFile);
                product.ProfileProduct = newProductImage;
            }

            if (model.UpdateProductDto.Images != null)
            {
                foreach (var file in model.UpdateProductDto.Images)
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

            product.Title = model.UpdateProductDto.Title;
            product.Avg = model.UpdateProductDto.Avg;
            product.BrandsId = model.UpdateProductDto.BrandsId;
            product.CategoryId = model.UpdateProductDto.CategoryId;
            product.ColorId = model.UpdateProductDto.ColorId;
            product.Discount = model.UpdateProductDto.Discount;
            product.Stock = model.UpdateProductDto.Stock;
            product.Price = model.UpdateProductDto.Price;
            product.Description = model.UpdateProductDto.Description;
            product.ProductMemory = model.UpdateProductDto.ProductMemory;
            product.ProductRam = model.UpdateProductDto.ProductRam;
            product.Higlist = model.UpdateProductDto.Higlist;

            _productWriteRepository.Update(product);
            await _productWriteRepository.SaveAsync();
            return true;
        }
    }
}
