using PastryShopAPI.Data.Entities;
using PastryShopAPI.Models.Combos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PastryShopAPI
{
    public interface IPastryShopRepository
    {
        //teams
        public Task<IEnumerable<CategoryEntity>> GetCategoriesAsync(string orderBy = "Id");
        public Task<CategoryEntity> GetCategoryAsync(long categoryId);
        public void CreateCategory(CategoryEntity newCategory);
        public Task DeleteCategoryAsync(long categoryId);
        public Task UpdateCategoryAsync(long categoryId, CategoryEntity updatedCategory);

        //Products
        public Task<IEnumerable<ProductEntity>> GetProductsAsync(long categoryId);
        public Task<ProductEntity> GetProductAsync(long categoryId, long productId);
        public void CreateProduct(long categoryId, ProductEntity newProduct);
        public Task DeleteProductAsync(long categoryId, long productId);
        public Task UpdateProductAsync(long categoryId, long productId, ProductEntity updatedProduct);

        //for combos:
        public Task<ProductEntity> GetProductAsync2(long productId);
        public Task<IEnumerable<ProductEntity>> GetAllProductsAsync();

        //Combos
        public void CreateCombo(ComboEntity comboEntity);
        public Task<IEnumerable<ComboEntity>> GetCombosAsync();
        public Task<ComboEntity> GetComboAsync(long comboId);
        public Task UpdateComboAsync(long? comboId, ComboEntity updatedCombo);
        public Task<bool> ProductIsInComboAsync(long productId, long comboId);

        public Task<IEnumerable<Product_ComboEntity>> GetProduct_CombosAsync();
        public void AddProduct_to_ComboAsync(Product_ComboEntity newProductCombo);//Task<Product_ComboEntity> AddProduct_to_ComboAsync(Product_ComboEntity newCombo);

        Task<bool> SaveChangesAsync();
    }
}
