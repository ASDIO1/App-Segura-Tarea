using PastryShopAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PastryShopAPI.Services
{
    public interface ICategoriesService
    {
        public Task<IEnumerable<CategoryModel>> GetCategoriesAsync(string orderBy = "Id");
        // public Task<TeamWithPlayerModel> GetTeamAsync(long teamId);
        public Task<CategoryModel> GetCategoryAsync(long categoryId);
        public Task<CategoryModel> CreateCategoryAsync(CategoryModel newCategory);
        public Task<bool> DeleteCategoryAsync(long categoryId);
        public Task<CategoryModel> UpdateCategoryAsync(long categoryId, CategoryModel updatedCategory);
    }
}
