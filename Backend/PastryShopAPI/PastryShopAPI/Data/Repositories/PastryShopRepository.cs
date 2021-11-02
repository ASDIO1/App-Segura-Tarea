using Microsoft.EntityFrameworkCore;
using PastryShopAPI.Data.Entities;
using PastryShopAPI.Models.Combos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PastryShopAPI.Data.Repositories
{
    public class PastryShopRepository : IPastryShopRepository
    {
        private PastryShopDbContext _dbContext;
        public PastryShopRepository(PastryShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // ================================ CATEGORIES =======================================
        public void CreateCategory(CategoryEntity newCategory)
        {
            _dbContext.Categories.Add(newCategory);
        }

        public async Task DeleteCategoryAsync(long categoryId)
        {
            var categoryToDelete = await _dbContext.Categories.FirstAsync(c => c.Id == categoryId);
            _dbContext.Categories.Remove(categoryToDelete);
        }

        public async Task<IEnumerable<CategoryEntity>> GetCategoriesAsync(string orderBy = "Id")
        {
            IQueryable<CategoryEntity> query = _dbContext.Categories;
            query = query.AsNoTracking();

            switch (orderBy.ToLower())
            {
                case "name":
                    query = query.OrderBy(c => c.Name);
                    break;
                case "description":
                    query = query.OrderBy(c => c.Description);
                    break;
                /*
                case "flavors":
                    query = query.OrderBy(c => c.Flavors);
                    break;*/
                default:
                    query = query.OrderBy(c => c.Id);
                    break;
            }

            return await query.ToListAsync();
        }

        public async Task UpdateCategoryAsync(long categoryId, CategoryEntity updatedCategory)
        {
            var category = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);

            category.Name = updatedCategory.Name ?? category.Name;
            category.Description = updatedCategory.Description ?? category.Description;
            category.ImageUrl = updatedCategory.ImageUrl ?? category.ImageUrl;
            category.ImagePath = updatedCategory.ImagePath ?? category.ImagePath;
            // category.Flavors = updatedCategory.Flavors ?? category.Flavors;
        }
        public async Task<CategoryEntity> GetCategoryAsync(long categoryId)
        {
            IQueryable<CategoryEntity> query = _dbContext.Categories;
            query = query.AsNoTracking();
            //query = query.Include(t => t.Products);
            return await query.FirstOrDefaultAsync(c => c.Id == categoryId);

            //hit to database
            //tolist()
            //toArray()
            //foreach
            //firstOfDefaul
            //Single
            //Count
        }
        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                var res = await _dbContext.SaveChangesAsync();
                return res > 0;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        // ================================ PRODUCTS =======================================
        public async Task<IEnumerable<ProductEntity>> GetProductsAsync(long categoryId)
        {
            IQueryable<ProductEntity> query = _dbContext.Products;
            query = query.Where(p => p.Category.Id == categoryId);
            query = query.Include(p => p.Category);
            query = query.AsNoTracking();
            return await query.ToListAsync();
        }

        public async Task<ProductEntity> GetProductAsync(long categoryId, long productId)
        {
            IQueryable<ProductEntity> query = _dbContext.Products;
            query = query.AsNoTracking();
            //query = query.Include(p => p.Team);
            return await query.FirstOrDefaultAsync(p => p.Category.Id == categoryId && p.Id == productId);
        }

        public async Task<ProductEntity> GetProductAsync2(long productId)
        {
            IQueryable<ProductEntity> query = _dbContext.Products;
            query = query.AsNoTracking();
            //query = query.Include(p => p.Team);
            return await query.FirstOrDefaultAsync(p => p.Id == productId);
        }

        public void CreateProduct(long categoryId, ProductEntity newProduct)
        {
            _dbContext.Entry(newProduct.Category).State = EntityState.Unchanged;
            _dbContext.Products.Add(newProduct);
        }

        public async Task DeleteProductAsync(long categoryId, long productId)
        {
            var productToDelete = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == productId);
            _dbContext.Remove(productToDelete);
        }

        public async Task UpdateProductAsync(long categoryId, long productId, ProductEntity updatedProduct)
        {
            var productToUpdate = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == productId);
            productToUpdate.Name = updatedProduct.Name ?? productToUpdate.Name;
            productToUpdate.Description = updatedProduct.Description ?? productToUpdate.Description;
            productToUpdate.Price = updatedProduct.Price ?? productToUpdate.Price;
            productToUpdate.Rating = updatedProduct.Rating ?? productToUpdate.Rating;
            productToUpdate.ImageUrl = updatedProduct.ImageUrl ?? productToUpdate.ImageUrl;
            productToUpdate.ImagePath = updatedProduct.ImagePath ?? productToUpdate.ImagePath;
        }

        public async Task<IEnumerable<ProductEntity>> GetAllProductsAsync()
        {
            IQueryable<ProductEntity> query = _dbContext.Products;
            query = query.AsNoTracking();
            return await query.ToListAsync();
        }


        // ================================ COMBOS =======================================
        public void CreateCombo(ComboEntity newComboEntity)
        {
            _dbContext.Combos.Add(newComboEntity);
        }

        public async Task<IEnumerable<ComboEntity>> GetCombosAsync()
        {
            IQueryable<ComboEntity> query = _dbContext.Combos;
            query = query.AsNoTracking();

            return await query.ToListAsync();
        }

        
        public async Task<ComboEntity> GetComboAsync(long comboId)
        {
            IQueryable<ComboEntity> query = _dbContext.Combos;
            query = query.AsNoTracking();
            //query = query.Include(t => t.Products);
            return await query.FirstOrDefaultAsync(c => c.Id == comboId);
        }

        public async Task UpdateComboAsync(long? comboId, ComboEntity updatedCombo)
        {
            var combo = await _dbContext.Combos.FirstOrDefaultAsync(c => c.Id == comboId);

            combo.Name = updatedCombo.Name ?? combo.Name;
            combo.Description = updatedCombo.Description ?? combo.Description;
            combo.Price = updatedCombo.Price ?? combo.Price;
            combo.ImageUrl = updatedCombo.ImageUrl ?? combo.ImageUrl;
            combo.ImagePath = updatedCombo.ImagePath ?? combo.ImagePath;
        }

        public async Task<bool> ProductIsInComboAsync(long productId, long comboId)
        {
            IQueryable<Product_ComboEntity> query = _dbContext.Product_Combos;
            var combo = await query.FirstOrDefaultAsync(pc => pc.ComboId == comboId && pc.ProductId == productId);
            if (combo == null)
            {
                return false;
            }
            return true;

        }

        public async Task<IEnumerable<Product_ComboEntity>> GetProduct_CombosAsync()
        {
            IQueryable<Product_ComboEntity> query = _dbContext.Product_Combos;
            query = query.AsNoTracking();

            return await query.ToListAsync();
        }


        // ====================== COMBOS - PRODUCTS  (MANY to MANY) ======================
        public void AddProduct_to_ComboAsync(Product_ComboEntity newProductCombo)
        {
            _dbContext.Products.Add(newProductCombo.Product);
            _dbContext.Products.Attach(newProductCombo.Product);

            _dbContext.Combos.Add(newProductCombo.Combo);
            _dbContext.Combos.Attach(newProductCombo.Combo);

            _dbContext.Product_Combos.Add(newProductCombo);
            //newProductCombo.Product.Product_Combos.Add(newProductCombo);

        }

        
    }
}
