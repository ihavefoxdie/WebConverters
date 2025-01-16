using Microsoft.AspNetCore.Mvc;
using Converters.Models;
using Converters.Api.Repositories.Interfaces;
using Converters.Api.Entities;

namespace Converters.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryRepository<ServiceCategory> _categoryRepository;


    public CategoryController(ICategoryRepository<ServiceCategory> categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }


    #region Post
    /// <summary>
    /// Adds a category object to the database.
    /// </summary>
    /// <param name="name">Category object's name</param>
    /// <returns>HTTP status code.</returns>
    [HttpPost]
    public async Task<ActionResult> AddCategory(string name)
    {
        try
        {
            ServiceCategory category = new()
            {
                Name = name
            };
            await _categoryRepository.AddCategory(category);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
        }

        return Ok();
    }
    #endregion


    #region Get
    /// <summary>
    /// Gets the category object that has the matching id from the database.
    /// </summary>
    /// <param name="id">ID of the category object to match.</param>
    /// <returns>The category object and an HTTP status code.</returns>
    [HttpGet]
    public async Task<ActionResult<ServiceCategoryDTO>> GetCategory(int id)
    {
        ServiceCategoryDTO itemDTO;
        try
        {
            ServiceCategory? item = await _categoryRepository.GetCategory(id);

            if (item == null)
            {
                return NotFound();
            }

            itemDTO = new()
            {
                Id = item.Id,
                Name = item.Name,
            };
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                            "Internal server error: " + e.Message);
        }

        return Ok(itemDTO);
    }

    /// <summary>
    /// Gets every category object from the database.
    /// </summary>
    /// <returns>The category object collection and an HTTP status code.</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ServiceCategoryDTO>>> GetCategories()
    {
        List<ServiceCategoryDTO> itemsDTO = [];
        try
        {
            IEnumerable<ServiceCategory>? items = await _categoryRepository.GetCategories();

            if (items == null)
            {
                return NotFound();
            }

            foreach (var item in items)
            {
                itemsDTO.Add(new ServiceCategoryDTO
                {
                    Id = item.Id,
                    Name = item.Name,
                });
            }
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                            "Internal server error: " + e.Message);
        }

        return Ok(itemsDTO);
    }
    #endregion


    #region Put
    /// <summary>
    /// Updates the category object that matches the ID in the database with the new data.
    /// </summary>
    /// <param name="id">ID of the category object to update.</param>
    /// <param name="name">Category object's new name.</param>
    /// <returns>HTTP status code.</returns>
    [HttpPut]
    public async Task<ActionResult> UpdateCategory(int id, string name)
    {
        try
        {
            ServiceCategory changedCategory = new()
            {
                Name = name,
                Id = id  
            };
            ServiceCategory? category = await _categoryRepository.UpdateCategory(changedCategory);

            if (category is null)
                return NotFound();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
        }

        return Ok();
    }
    #endregion


    #region Delete
    /// <summary>
    /// Deletes the category object with the matching ID from the database.
    /// </summary>
    /// <param name="id">ID of the category object to delete.</param>
    /// <returns>HTTP status code.</returns>
    [HttpDelete]
    public async Task<ActionResult> DeleteCategory(int id)
    {
        try
        {
            ServiceCategory? category = await _categoryRepository.DeleteCategory(id);

            if (category is null)
                return NotFound();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
        }

        return Ok();
    }
    #endregion
}
