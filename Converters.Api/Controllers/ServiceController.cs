using Converters.Api.Repositories.Interfaces;
using Converters.Api.Entities;
using Microsoft.AspNetCore.Mvc;
using Converters.Models;

namespace Converters.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class ServiceController : ControllerBase
{
    private readonly IServiceRepository<Service> _serviceRepository;

    public ServiceController(IServiceRepository<Service> serviceRepository)
    {
        _serviceRepository = serviceRepository;
    }

    [HttpPost]
    public async Task<ActionResult> AddService(string name, int categoryId, string type, string description, string address)
    {
        try
        {
            await _serviceRepository.AddItem(name, categoryId, type, description, address);
        }
        catch(Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
        }
        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult<ServiceDTO>> GetService(int id)
    {
        try
        {
            Service item = await _serviceRepository.GetItem(id);
            if (item == null)
            {
                return NotFound();
            }
            ServiceDTO itemDTO = new()
            {
                Id = item.Id,
                CategoryId = item.CategoryId,
                Name = item.Name,
                Type = item.Type,
                CategoryName = item.ServiceCategory.Name,
                Description = item.Description,
                Address = item.Address
            };

            return Ok(itemDTO);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                            $"Internal server error: {e.Message}");
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ServiceDTO>>> GetServices()
    {
        try
        {
            IEnumerable<Service> items = await _serviceRepository.GetItems();

            if (items == null)
            {
                return NotFound();
            }

            List<ServiceDTO> itemsDTO = [];
            foreach (var item in items)
            {
                itemsDTO.Add(new ServiceDTO
                {
                    Id = item.Id,
                    CategoryId = item.CategoryId,
                    Name = item.Name,
                    Type = item.Type,
                    CategoryName = item.ServiceCategory.Name,
                    Description = item.Description,
                    Address = item.Address
                });
            }

            return Ok(itemsDTO);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                            "Internal server error: " + e.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<ServiceCategoryDTO>> GetCategory(int id)
    {
        try
        {
            ServiceCategory item = await _serviceRepository.GetCategory(id);
            if (item == null)
            {
                return NotFound();
            }
            ServiceCategoryDTO itemDTO = new()
            {
                Id = item.Id,
                Name = item.Name,
            };

            return Ok(itemDTO);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                            "Internal server error: " + e.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ServiceCategoryDTO>>> GetCategories()
    {
        try
        {
            IEnumerable<ServiceCategory> items = await _serviceRepository.GetCategories();

            if (items == null)
            {
                return NotFound();
            }

            List<ServiceCategoryDTO> itemsDTO = [];
            foreach (var item in items)
            {
                itemsDTO.Add(new ServiceCategoryDTO
                {
                    Id = item.Id,
                    Name = item.Name,
                });
            }

            return Ok(itemsDTO);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                            "Internal server error: " + e.Message);
        }
    }
}
