using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PastryShopAPI.Exceptions;
using PastryShopAPI.Models;
using PastryShopAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PastryShopAPI.Controllers
{
    [Route("api/categories/{categoryId:long}/[controller]")]
    public class ProductsController : Controller
    {
        private IProductsService _productService;
        private IFileService _fileService; // Files (image files) handler service

        public ProductsController(IProductsService productService, IFileService fileService)
        {
            _productService = productService;
            _fileService = fileService;
        }

        //localhost:3030/api/teams/{teamId:long}/players
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductModel>>> GetProductsAsync(long categoryId)
        {
            try
            {
                var products = await _productService.GetProductsAsync(categoryId);
                return Ok(products);
            }
            catch (NotFoundItemException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something unexpected happened.");
            }
        }

        [HttpGet("{productId:long}")]
        public async Task<IActionResult> GetProductAsync(long categoryId, long productId)
        {
            try
            {
                var product = await _productService.GetProductAsync(categoryId, productId);
                return Ok(product);
            }
            catch (NotFoundItemException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception /*ex*/)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something unexpected happened.");
            }

        }

        /*[HttpPost]
        public async Task<ActionResult<ProductModel>> CreateProductAsync(long categoryId, [FromBody] ProductModel newProduct)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var createdProduct = await _productService.CreateProductAsync(categoryId, newProduct);
                return Created($"/api/categories/{categoryId}/products/{createdProduct.Id}", createdProduct);
            }
            catch (NotFoundItemException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something unexpected happened.");
            }
        }*/

        [HttpPost("Form")]
        public async Task<ActionResult<ProductModel>> CreateProductFormAsync(long categoryId, [FromForm] ProductFormModel newProduct)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var file = newProduct.Image;
                string imagePath = _fileService.UploadFile(file);

                newProduct.ImagePath = imagePath;

                var createdProduct = await _productService.CreateProductAsync(categoryId, newProduct);
                return Created($"/api/categories/{categoryId}/products/{createdProduct.Id}", createdProduct);
            }
            catch (NotFoundItemException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception /*ex*/)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something unexpected happened.");
            }
        }

        [HttpDelete("{productId:int}")]
        public async Task<ActionResult<bool>> DeleteProductAsync(long categoryId, long productId)
        {
            try
            {
                var result = await _productService.DeleteProductAsync(categoryId, productId);
                return Ok(result);
            }
            catch (NotFoundItemException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something unexpected happened.");
            }
        }

        [HttpPut("{productId:long}")]
        public async Task<ActionResult<ProductModel>> UpdateProductAsync(long categoryId, long productId, [FromBody] ProductModel productToUpdate)
        {
            try
            {
                var updatedProduct = await _productService.UpdateProductAsync(categoryId, productId, productToUpdate);
                return Ok(updatedProduct);
            }
            catch (NotFoundItemException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something unexpected happened.");
            }
        }

        [HttpPut("{productId:long}/Form")]
        public async Task<ActionResult<ProductModel>> UpdateProductFormAsync(long categoryId, long productId, [FromForm] ProductFormModel productToUpdate)
        {
            try
            {
                var file = productToUpdate.Image;
                string imagePath = _fileService.UploadFile(file);

                productToUpdate.ImagePath = imagePath;

                var updatedProduct = await _productService.UpdateProductAsync(categoryId, productId, productToUpdate);
                var response =  Ok(updatedProduct);
                return response; //Returns code 200 ok but JavaScript throws error 500 internal server error???
            }
            catch (NotFoundItemException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something unexpected happened.");
            }
        }


        // For Combo Domain/Resource
        [HttpGet("All")]
        public async Task<ActionResult<IEnumerable<ProductModel>>> GetAllProductsAsync(long categoryId=0)
        {
            try
            {
                var products = await _productService.GetAllProductsAsync(categoryId);
                return Ok(products);
            }
            catch (NotFoundItemException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something unexpected happened.");
            }
        }
    }
}
