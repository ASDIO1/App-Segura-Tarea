using AutoMapper;
using PastryShopAPI.Data.Entities;
using PastryShopAPI.Exceptions;
using PastryShopAPI.Models;
using PastryShopAPI.Models.Combos;
using PastryShopAPI.Models.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PastryShopAPI.Services.Combos
{
    public class CombosService : ICombosService
    {
        private IPastryShopRepository _pastryShopRepository;
        private IMapper _mapper;

        public CombosService(IPastryShopRepository pastryShopRepository, IMapper mapper)
        {
            _pastryShopRepository = pastryShopRepository;
            _mapper = mapper;
        }

        // ====================== COMBO ENDPOINTS ======================

        public async Task<ComboModel> CreateComboAsync(ComboModel newCombo)
        {
            var comboEntity = _mapper.Map<ComboEntity>(newCombo);
            _pastryShopRepository.CreateCombo(comboEntity);
            var result = await _pastryShopRepository.SaveChangesAsync();

            if (result)
            {
                return _mapper.Map<ComboModel>(comboEntity);
            }

            throw new Exception("Database Error");
        }

        public async Task<IEnumerable<ComboModel>> GetCombosAsync()
        {
            var entityList = await _pastryShopRepository.GetCombosAsync();
            var modelList = _mapper.Map<IEnumerable<ComboModel>>(entityList);
            return modelList;
        }

        public async Task<ComboModel> GetComboAsync(long comboId)
        {
            var combo = await _pastryShopRepository.GetComboAsync(comboId);

            if (combo == null)
            {
                throw new NotFoundItemException($"The combo with id: {comboId} does not exists.");
            }


            return _mapper.Map<ComboModel>(combo);
        }

        public async Task<ComboModel> UpdateComboAsync(/*long comboId,*/ ComboModel updatedCombo)
        {
            //await ValidateComboAsync(comboId);// Quizas ya no es necesario!
            //updatedCombo.Id = comboId;
            await _pastryShopRepository.UpdateComboAsync(updatedCombo.Id, _mapper.Map<ComboEntity>(updatedCombo));
            var result = await _pastryShopRepository.SaveChangesAsync();

            if (!result)
            {
                throw new Exception("Database Error");
            }

            return updatedCombo;
        }


        // ====================== COMBOS - PRODUCTS  (MANY to MANY) ======================


        public async Task<UserManagerResponse> CreateProductComboAsync(Product_ComboModel productcomboModel)
        {
            var productId = productcomboModel.ProductId;
            var comboId = productcomboModel.ComboId;
            var product = await _pastryShopRepository.GetProductAsync2(productId); // Tratar de mejorar esto (quizas no sea posible, pero intenterlo luego)
            var combo = await GetComboAsync(comboId);

            // Check product and combo existance
            if (product == null)
            {
                return new UserManagerResponse
                {
                    Message = "Product does not exist",
                    IsSuccess = false
                };
            }
            if (combo == null)
            {
                return new UserManagerResponse
                {
                    Message = "Combo does not exist",
                    IsSuccess = false
                };
            }

            // Check if combo already has the product
            if (await _pastryShopRepository.ProductIsInComboAsync(productId, comboId))
            {
                return new UserManagerResponse
                {
                    Message = "combo has product already",
                    IsSuccess = false
                };
            }

            // Returning success or exception error
            var productComboEntity = _mapper.Map<Product_ComboEntity>(productcomboModel);
            productComboEntity.Product = product;
            productComboEntity.Combo = _mapper.Map<ComboEntity>(combo);
            //product.Product_Combos.Add(productComboEntity); // links product to combos (via produt_combo table). (Esto se hace en el repository usando dbContext!!!)

            _pastryShopRepository.AddProduct_to_ComboAsync(productComboEntity);
            var result = await _pastryShopRepository.SaveChangesAsync();
            if (result == true)
            {
                return new UserManagerResponse
                {
                    Message = "Product assigned to combo",
                    IsSuccess = true
                };
            }

            return new UserManagerResponse
            {
                Message = "something went wrong",
                IsSuccess = false
            };
        }


        public async Task<long> CalculateComboPrice(long comboId)
        {
            long totalPrice = 0;
            IEnumerable<Product_ComboEntity> productCombos = await _pastryShopRepository.GetProduct_CombosAsync();
            var comboProducts = productCombos.Where(pc => pc.ComboId == comboId);
            foreach (var comboProduct in comboProducts)
            {
                var product = await _pastryShopRepository.GetProductAsync2(comboProduct.ProductId);
                var price = product.Price ?? 0; //If price == null then its 0
                totalPrice = price + totalPrice;
                
            }
            return totalPrice;
        }

        private async Task ValidateComboAsync(long comboId)
        {
            // await GetCategoryAsync(teamId);
            var combo = await _pastryShopRepository.GetComboAsync(comboId);

            if (combo == null)
            {
                throw new NotFoundItemException($"The combo with id: {comboId} does not exists.");
            }
        }


        public Task<IEnumerable<ProductModel>> GetAllComboProductsAsync(long comboId)
        {
            throw new NotImplementedException();
        }
    }
}
