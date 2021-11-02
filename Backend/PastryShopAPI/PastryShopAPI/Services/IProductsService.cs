using PastryShopAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PastryShopAPI.Services
{
    public interface IProductsService
    {
        public Task<IEnumerable<ProductModel>> GetProductsAsync(long categoriyId);
        public Task<ProductModel> GetProductAsync(long categoriyId, long productId);
        public Task<ProductModel> CreateProductAsync(long categoriyId, ProductModel newProduct);
        public Task<bool> DeleteProductAsync(long categoriyId, long productId);
        public Task<ProductModel> UpdateProductAsync(long categoriyId, long productId, ProductModel updatedProduct);

        // For Combos
        public Task<IEnumerable<ProductModel>> GetAllProductsAsync(long categoriyId); 
    }
}
