using Converters.Api.Repositories.Interfaces;
using Converters.Api.Entities;
using Microsoft.AspNetCore.Mvc;
using Converters.Models;

namespace Converters.Api.Controllers;

//TODO: separate category related actions into a new controller
[Route("api/[controller]/[action]")]
[ApiController]
public class ServiceController : ControllerBase
{
    private readonly IServiceRepository<Service> _serviceRepository;


    public ServiceController(IServiceRepository<Service> serviceRepository, ICategoryRepository<ServiceCategory> categoryRepository)
    {
        _serviceRepository = serviceRepository;
    }


    #region Post
    [HttpPost]
    public async Task<ActionResult> AddService(string name, int categoryId, string type, string description, string address)
    {
        try
        {
            Service service = new()
            {
                Name = name,
                CategoryId = categoryId,
                Type = type,
                Description = description,
                Address = address
            };
            await _serviceRepository.AddItem(service);
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
    #endregion

    #region Put
    [HttpPut]
    public async Task<ActionResult> UpdateItem(int id, string name, int categoryId, string type, string description, string address)
    {
        try
        {
            Service service = new()
            {
                Id = id,
                Name = name,
                CategoryId = categoryId,
                Type = type,
                Description = description,
                Address = address
            };
            Service? updatedService = await _serviceRepository.UpdateItem(service);

            if (updatedService is null)
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
    #endregion
}
