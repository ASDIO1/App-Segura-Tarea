using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PastryShopAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PastryShopAPI.Data
{
    public class PastryShopDbContext : IdentityDbContext
    {
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<ProductEntity> Products { get; set; }

        public DbSet<ComboEntity> Combos { get; set; }
        public DbSet<Product_ComboEntity> Product_Combos { get; set; } //COMBO-PRODUCT RELATIONSHIP

        public PastryShopDbContext(DbContextOptions<PastryShopDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ====== Categories (1-->M) ======
            modelBuilder.Entity<CategoryEntity>().ToTable("Categories");
            modelBuilder.Entity<CategoryEntity>().Property(c => c.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<CategoryEntity>().HasMany(c => c.Products).WithOne(p => p.Category);

            // ====== Products (1-->M) ======
            modelBuilder.Entity<ProductEntity>().ToTable("Products");
            modelBuilder.Entity<ProductEntity>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<ProductEntity>().HasOne(p => p.Category).WithMany(c => c.Products);
            // Many to Many relationship is defined in the Product_Combo Entity modelBuilder.


            // ====== Combos ======
            modelBuilder.Entity<ComboEntity>().ToTable("Combos");
            modelBuilder.Entity<ComboEntity>().Property(c => c.Id).ValueGeneratedOnAdd();
            // Many to Many relationship is defined in the Product_Combo Entity modelBuilder.


            // ====== Key definitions Product_Combo ======
            /*modelBuilder.Entity<Product_ComboEntity>()
            .HasKey(bc => new { bc.ProductId, bc.ComboId });*/


            // ====== Combos - Products (M-->M) ======
            modelBuilder.Entity<Product_ComboEntity>()
            .HasOne(p => p.Product)
            .WithMany(pc => pc.Product_Combos)
            .HasForeignKey(ci => ci.ProductId);

            modelBuilder.Entity<Product_ComboEntity>()
            .HasOne(c => c.Combo)
            .WithMany(pc => pc.Product_Combos)
            .HasForeignKey(pi => pi.ComboId);

            //dotnet tool install --global dotnet-ef
            //dotnet ef migrations add InitialCreate
            //dotnet ef database update
        }
    }
}
