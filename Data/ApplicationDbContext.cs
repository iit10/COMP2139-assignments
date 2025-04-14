using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartInventory3.Models;

namespace SmartInventory3.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<Product> Products => Set<Product>();
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("Identity");
            
            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderItems)
                .WithOne(o => o.Order)
                .HasForeignKey(o => o.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // seed categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Electronics", Description = "Electronic items" },
                new Category { Id = 2, Name = "Books", Description = "Books" },
                new Category { Id = 3, Name = "Food", Description = "Groceries" }
            );

            // seed products
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Senheiser 620S", CategoryId = 1, Price = 620.00M, Quantity = 10, LowStockThreshold = 5 },
                new Product { Id = 2, Name = "Brave New World", CategoryId = 2, Price = 12.0M, Quantity = 100, LowStockThreshold = 14 },
                new Product { Id = 3, Name = "Peanut Butter", CategoryId = 3, Price = 1.0M, Quantity = 1000, LowStockThreshold = 24 }
            );
            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable("Users");
            });
            modelBuilder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable("Role");
            });
            modelBuilder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("USerRole");
            });
            modelBuilder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaim");
            });
            modelBuilder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserToken");
            });
            modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogin");
            });
            modelBuilder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("RoleClaim");
            });
            

        }
        
    }
}