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
