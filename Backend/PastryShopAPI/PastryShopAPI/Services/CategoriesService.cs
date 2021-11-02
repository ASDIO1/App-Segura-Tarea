using AutoMapper;
using PastryShopAPI.Exceptions;
using PastryShopAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PastryShopAPI.Services
{
    public class CategoriesService : ICategoriesService
    {
        private IPastryShopRepository _pastryShopRepository;
        private IMapper _mapper;

        private HashSet<string> _allowedOrderByValues = new HashSet<string>()
        {
            "id",
            "name",
            "price",
            "flavors"
        };

        public CategoriesService(IPastryShopRepository pastryShopRepository, IMapper mapper)
        {
            _pastryShopRepository = pastryShopRepository;
            _mapper = mapper;
        }

        public async Task<CategoryModel> CreateCategoryAsync(CategoryModel newCategory)
        {
            var categoryEntity = _mapper.Map<CategoryEntity>(newCategory);
            _pastryShopRepository.CreateCategory(categoryEntity);
            var result = await _pastryShopRepository.SaveChangesAsync();

            if (result)
            {
                return _mapper.Map<CategoryModel>(categoryEntity);
            }

            throw new Exception("Database Error");
        }

        public async Task<bool> DeleteCategoryAsync(long categoryId)
        {
            await ValidateCategoryAsync(categoryId);
            await _pastryShopRepository.DeleteCategoryAsync(categoryId);
            var result = await _pastryShopRepository.SaveChangesAsync();

            if (!result)
            {
                throw new Exception("Database Error");
            }
            return true;
        }

        public async Task<IEnumerable<CategoryModel>> GetCategoriesAsync(string orderBy = "Id")
        {
            if (!_allowedOrderByValues.Contains(orderBy.ToLower()))
                throw new InvalidOperationItemException($"The Orderby value: {orderBy} is invalid, please use one of {String.Join(',', _allowedOrderByValues.ToArray())}");
            var entityList = await _pastryShopRepository.GetCategoriesAsync(orderBy.ToLower());
            var modelList = _mapper.Map<IEnumerable<CategoryModel>>(entityList);
            return modelList;
        }

        public async Task<CategoryModel> GetCategoryAsync(long categoryId)
        {
            var category = await _pastryShopRepository.GetCategoryAsync(categoryId);

            if (category == null)
            {
                throw new NotFoundItemException($"The category with id: {categoryId} does not exists.");
            }


            return _mapper.Map<CategoryModel>(category); // _mapper.Map<CategoryFormModel>(category);
        }

        public async Task<CategoryModel> UpdateCategoryAsync(long categoryId, CategoryModel updatedCategory)
        {
            await ValidateCategoryAsync(categoryId);// GetTeamAsync(teamId);
            updatedCategory.Id = categoryId;
            await _pastryShopRepository.UpdateCategoryAsync(categoryId, _mapper.Map<CategoryEntity>(updatedCategory));
            var result = await _pastryShopRepository.SaveChangesAsync();

            if (!result)
            {
                throw new Exception("Database Error");
            }

            return updatedCategory;//_mapper.Map<CategoryModel>(updatedCategory);
        }

        private async Task ValidateCategoryAsync(long categoryId)
        {
            // await GetCategoryAsync(teamId);
            var category = await _pastryShopRepository.GetCategoryAsync(categoryId);

            if (category == null)
            {
                throw new NotFoundItemException($"The category with id: {categoryId} does not exists.");
            }
        }
    }
}
