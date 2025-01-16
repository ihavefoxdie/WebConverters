using Converters.Api.Repositories.Interfaces;
using Converters.Api.Entities;
using Microsoft.AspNetCore.Mvc;
using Converters.Models;

namespace Converters.Api.Controllers;

//TODO: Create an interface (something like ICRUDController?) and inherit from it?..
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
    /// <summary>
    /// Adds a service object to the database.
    /// </summary>
    /// <param name="name">Service object's name</param>
    /// <returns>HTTP status code.</returns>
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
    /// <summary>
    /// Gets the service object that has the matching id from the database.
    /// </summary>
    /// <param name="id">ID of the service object to match.</param>
    /// <returns>The service object and an HTTP status code.</returns>
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

    /// <summary>
    /// Gets every category object from the database.
    /// </summary>
    /// <returns>The category object collection and an HTTP status code.</returns>
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
    /// <summary>
    /// Updates the service object that matches the ID in the database with the new data.
    /// </summary>
    /// <param name="id">ID of the service object to update.</param>
    /// <param name="name">Name of the service object to update.</param>
    /// <param name="categoryId">Category ID of the service object to update.</param>
    /// <param name="type">Type of the service object to update.</param>
    /// <param name="description">Description of the service object to update.</param>
    /// <param name="address">Address of the service object to update.</param>
    /// <returns></returns>
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
    /// <summary>
    /// Deletes the service object with the matching ID from the database.
    /// </summary>
    /// <param name="id">ID of the service object to delete.</param>
    /// <returns>HTTP status code.</returns>    
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
