using Converters.Api.Repositories.Interfaces;
using Converters.Api.Entities;
using Microsoft.AspNetCore.Mvc;
using Converters.Models;
using Microsoft.AspNetCore.Http.Features;

namespace Converters.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class ServiceController : ControllerBase
{
    private readonly IServiceRepository<Service, ServiceCategory> _serviceRepository;

    public ServiceController(IServiceRepository<Service, ServiceCategory> serviceRepository)
    {
        _serviceRepository = serviceRepository;
    }



    #region Post
    [HttpPost]
    public async Task<ActionResult> AddService(string name, int categoryId, string type, string description, string address)
    {
        try
        {
            await _serviceRepository.AddItem(name, categoryId, type, description, address);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
        }

        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult> AddCategory(string name)
    {
        try
        {
            await _serviceRepository.AddCategory(name);
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
    public async Task<ActionResult<ServiceDTO>> GetService(int id)
    {
        ServiceDTO itemDTO;
        try
        {
            Service? item = await _serviceRepository.GetItem(id);
            if (item == null)
            {
                return NotFound();
            }

            string categoryName;
                if(item.ServiceCategory is not null)
                    categoryName = item.ServiceCategory.Name;
                else
                    categoryName = "Uncategorized";

            itemDTO = new()
            {
                Id = item.Id,
                CategoryId = item.CategoryId,
                Name = item.Name,
                Type = item.Type,
                CategoryName = categoryName,
                Description = item.Description,
                Address = item.Address
            };
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                            $"Internal server error: {e.Message}");
        }

        return Ok(itemDTO);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ServiceDTO>>> GetServices()
    {
        List<ServiceDTO> itemsDTO = [];
        try
        {
            IEnumerable<Service>? items = await _serviceRepository.GetItems();

            if (items == null)
            {
                return NotFound();
            }

            foreach (var item in items)
            {
                string categoryName;
                if(item.ServiceCategory is not null)
                    categoryName = item.ServiceCategory.Name;
                else
                    categoryName = "Uncategorized";

                itemsDTO.Add(new ServiceDTO
                {
                    Id = item.Id,
                    CategoryId = item.CategoryId,
                    Name = item.Name,
                    Type = item.Type,
                    CategoryName = categoryName,
                    Description = item.Description,
                    Address = item.Address
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

    [HttpGet]
    public async Task<ActionResult<ServiceCategoryDTO>> GetCategory(int id)
    {
        ServiceCategoryDTO itemDTO;
        try
        {
            ServiceCategory? item = await _serviceRepository.GetCategory(id);

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
            IEnumerable<ServiceCategory>? items = await _serviceRepository.GetCategories();

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
    public async Task<ActionResult> UpdateItem(int id, string name, int categoryId, string type, string description, string address)
    {
        try
        {
            Service? service = await _serviceRepository.UpdateItem(id, name, categoryId, type, description, address);

            if (service is null)
                return NotFound();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
        }

        return Ok();
    }

    [HttpPut]
    public async Task<ActionResult> UpdateCategory(int id, string name)
    {
        try
        {
            ServiceCategory? category = await _serviceRepository.UpdateCategory(id, name);

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
    public async Task<ActionResult> DeleteItem(int id)
    {
        try
        {
            Service? service = await _serviceRepository.DeleteItem(id);

            if (service is null)
                return NotFound();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
        }

        return Ok();
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteCategory(int id)
    {
        try
        {
            ServiceCategory? category = await _serviceRepository.DeleteCategory(id);

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
