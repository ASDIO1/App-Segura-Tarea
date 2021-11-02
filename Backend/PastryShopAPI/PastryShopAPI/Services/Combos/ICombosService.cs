using PastryShopAPI.Models;
using PastryShopAPI.Models.Combos;
using PastryShopAPI.Models.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PastryShopAPI.Services.Combos
{
    public interface ICombosService
    {
        Task<ComboModel> CreateComboAsync(ComboModel model);
        public Task<IEnumerable<ComboModel>> GetCombosAsync();
        public Task<ComboModel> GetComboAsync(long comboId);
        public Task<ComboModel> UpdateComboAsync(/*long comboId,*/ ComboModel updatedCombo);
        public Task<long> CalculateComboPrice(long comboId);


        // Many to many methods
        public Task<UserManagerResponse> CreateProductComboAsync(Product_ComboModel productcomboModel);
        public Task<IEnumerable<ProductModel>> GetAllComboProductsAsync(long comboId);
    }
}
