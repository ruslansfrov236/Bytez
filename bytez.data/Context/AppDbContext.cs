using bytez.entity.Entities;
using bytez.entity.Entities.Customer;
using bytez.entity.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace bytez.data.Context
{
    public class AppDbContext : IdentityDbContext<AppUser, IdentityRole , string>
    {
       
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Product>? Products { get; set; }
        public DbSet<ProductImage>? ProductImages { get; set; }
        public DbSet<ProductBasket>? ProductBasket { get; set; }
        public DbSet<Basket>? Basket { get; set; }
        public DbSet<ProductColor>? ProductColor { get; set; }
        public DbSet<Order>? Order { get; set; }
        public DbSet<Header>? Header { get; set; }
        public DbSet<About>? About { get; set; }
        public DbSet<BrandModel>? Brand { get; set; }
        public DbSet<Category>? Categories { get; set; }
        public DbSet<ConnectionInfo>? ConnectionInfos { get; set; }
        public DbSet<Email>? Emails { get; set; }
        public DbSet<Message>? Messages { get; set; } 
        public DbSet<ContactCall>? ContactCalls { get; set; }

        public DbSet<OrderComponent>? OrderComponents { get; set; }
        public DbSet<Delivery>? Deliveries { get; set; }
        public DbSet<Cupon>? Cupons { get; set; }
        public DbSet<Wishlist>? Wishlists { get; set; }
        public DbSet<ContactWall>? ContactWalls { get; set; }    
        public DbSet<Blog>? Blogs { get; set; }

        
        public DbSet<Comment>? Comments { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
           
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {


            var datas = ChangeTracker
                 .Entries<BaseEntity>();

            foreach (var data in datas)
            {
                _ = data.State switch
                {
                    EntityState.Added => data.Entity.CreatedDate = DateTime.UtcNow,
                    EntityState.Modified => data.Entity.UpdatedDate = DateTime.UtcNow,
                    _ => DateTime.UtcNow
                };
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
  
}
