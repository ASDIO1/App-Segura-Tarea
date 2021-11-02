using AutoMapper;
using PastryShopAPI.Data.Entities;
using PastryShopAPI.Exceptions;
using PastryShopAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PastryShopAPI.Services
{
    public class ProductsService : IProductsService
    {
        private IPastryShopRepository _pastryShopRepository;
        private IMapper _mapper;

        public ProductsService(IPastryShopRepository pastryShopRepository, IMapper mapper)
        {
            _pastryShopRepository = pastryShopRepository;
            _mapper = mapper;
        }


        public async Task<ProductModel> CreateProductAsync(long categoryId, ProductModel newProduct)
        {
            await ValidateCategoryAsync(categoryId);
            newProduct.CategoryId = categoryId;
            var productEntity = _mapper.Map<ProductEntity>(newProduct);

            _pastryShopRepository.CreateProduct(categoryId, productEntity);

            var result = await _pastryShopRepository.SaveChangesAsync();

            if (!result)
            {
                throw new Exception("Database Error");
            }

            return _mapper.Map<ProductModel>(productEntity);
        }

        public async Task<bool> DeleteProductAsync(long categoryId, long productId)
        {
            await ValidateCategoryAndProductAsync(categoryId, productId);
            await _pastryShopRepository.DeleteProductAsync(categoryId, productId);

            var result = await _pastryShopRepository.SaveChangesAsync();

            if (!result)
            {
                throw new Exception("Database Error");
            }

            return true;
        }

        public async Task<ProductModel> GetProductAsync(long categoryId, long productId)
        {
            await ValidateCategoryAsync(categoryId);
            var productEntity = await _pastryShopRepository.GetProductAsync(categoryId, productId);
            if (productEntity == null)
            {
                throw new NotFoundItemException($"The product with id: {productId} does not exist in category with id:{categoryId}.");
            }

            var productModel = _mapper.Map<ProductModel>(productEntity);

            productModel.Id = categoryId;
            return productModel;
        }

        public async Task<IEnumerable<ProductModel>> GetProductsAsync(long categoryId)
        {
            await ValidateCategoryAsync(categoryId);
            var products = await _pastryShopRepository.GetProductsAsync(categoryId);
            return _mapper.Map<IEnumerable<ProductModel>>(products);
        }

        public async Task<ProductModel> UpdateProductAsync(long categoryId, long productId, ProductModel updatedProduct)
        {
            await ValidateCategoryAndProductAsync(categoryId, productId);
            await _pastryShopRepository.UpdateProductAsync(categoryId, productId, _mapper.Map<ProductEntity>(updatedProduct));
            var result = await _pastryShopRepository.SaveChangesAsync();

            if (!result)
            {
                throw new Exception("Database Error");
            }

            return updatedProduct;
        }

        private async Task ValidateCategoryAsync(long categoryId)
        {
            var category = await _pastryShopRepository.GetCategoryAsync(categoryId); // Reemplazar con GetCategoryAndProducts()  hacer este endpoint!
            if (category == null)
            {
                throw new NotFoundItemException($"The category with id: {categoryId} does not exists.");
            }
        }

        private async Task ValidateCategoryAndProductAsync(long categoryId, long productId)
        {
            var product = await GetProductAsync(categoryId, productId);
        }

        // FOR COMBOS Creation
        public async Task<IEnumerable<ProductModel>> GetAllProductsAsync(long categoryId)
        {
            await ValidateCategoryAsync(categoryId);
            var products = await _pastryShopRepository.GetAllProductsAsync();
            return _mapper.Map<IEnumerable<ProductModel>>(products);
        }
    }
}
