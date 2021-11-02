using PastryShopAPI.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PastryShopAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PastryShopAPI.Services;
using Microsoft.AspNetCore.Authorization;

namespace PastryShopAPI.Controllers
{
    
    [Route("api/[controller]")]
    public class CategoriesController : Controller
    {
        private ICategoriesService _categoriesService;
        private IFileService _fileService; // Files (image files) handler service

        public CategoriesController(ICategoriesService categoriesService, IFileService fileService)
        {
            _categoriesService = categoriesService;
            _fileService = fileService;
        }

        // api/teams
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryModel>>> GetCategoriesAsync(string orderBy = "Id")
        {
            try
            {
                var categories = await _categoriesService.GetCategoriesAsync(orderBy);
                return Ok(categories);
            }
            catch (InvalidOperationItemException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something unexpected happened.");
            }
        }

        // api/teams/2
        //[Authorize(Roles = "Admin")]
        [HttpGet("{categoryId:long}")]
        public async Task<ActionResult<CategoryModel>> GetCategoryAsync(long categoryId)// public async Task<ActionResult<CategoryWithProductModel>> GetCategoryAsync(long teamId)
        {
            try
            {
                var team = await _categoriesService.GetCategoryAsync(categoryId);
                return Ok(team);
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

        // REGULAR CREATE WITH imageUrl and JSON
        // api/teams
        /*[HttpPost]
        public async Task<ActionResult<CategoryModel>> CreateCategoryAsync([FromBody] CategoryModel newCategory)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var category = await _categoriesService.CreateCategoryAsync(newCategory);
                return Created($"/api/categories/{category.Id}", category);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something unexpected happened.");
            }
        }*/

        // CREATE CATEGORY WITH IMAGE FILE   api/categories/Form  using  FORM (no JSON)
        [HttpPost("Form")]
        public async Task<ActionResult<CategoryModel>> CreateCategoryFormAsync([FromForm] CategoryFormModel newCategory)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var file = newCategory.Image;
                string imagePath = _fileService.UploadFile(file);

                newCategory.ImagePath = imagePath;

                var category = await _categoriesService.CreateCategoryAsync(newCategory);
                return Created($"/api/categories/{category.Id}", category);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something unexpected happened.");
            }
        }

        [HttpDelete("{categoryId:long}")]
        public async Task<ActionResult<bool>> DeleteCategoryAsync(long categoryId)
        {
            try
            {
                var result = await _categoriesService.DeleteCategoryAsync(categoryId);
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

        [HttpPut("{categoryId:long}")]
        public async Task<ActionResult<CategoryModel>> UpdateCategoryAsync(long categoryId, [FromBody] CategoryModel updatedCategory)
        {
            try
            {
                /*if (!ModelState.IsValid)
                {
                    foreach (var pair in ModelState)
                    {
                        if (pair.Key == nameof(updatedTeam.City) && pair.Value.Errors.Count > 0)
                        {
                            return BadRequest(pair.Value.Errors);
                        }
                    }
                }*/

                var category = await _categoriesService.UpdateCategoryAsync(categoryId, updatedCategory);
                return Ok(category);
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

        [HttpPut("{categoryId:long}/Form")] // Probar usando POST o devolviendo un Booleano
        public async Task<ActionResult<CategoryModel>> UpdateCategoryFormAsync(long categoryId, [FromForm] CategoryFormModel updatedCategory)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var newFile = updatedCategory.Image;
                /*var actualFile = await _categoriesService.GetCategoryAsync(categoryId);
                if (_fileService.IsNewFile(actualFile.Image, newFile))
                {

                }*/
                string imagePath = _fileService.UploadFile(newFile);

                updatedCategory.ImagePath = imagePath;

                var category = await _categoriesService.UpdateCategoryAsync(categoryId, updatedCategory);
                return Ok(category);
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
