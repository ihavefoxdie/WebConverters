using Converters.Api.Repositories.Interfaces;
using Converters.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Converters.Api.Data;

namespace Converters.Api.Repositories;
public class ServiceRepositry : IServiceRepository<Service>
{
    private readonly ServicesDbContext _servicesDbContext;


    public ServiceRepositry(ServicesDbContext servicesDbContext)
    {
        _servicesDbContext = servicesDbContext;
    }


    public async Task AddItem(Service service)
    {
        await _servicesDbContext.Services.AddAsync(service);
        await SaveChangesAsync();
    }

    public async Task<Service?> GetItem(int id)
    {
        return await _servicesDbContext.Services.Include(x => x.ServiceCategory).FirstOrDefaultAsync(x => x.Id == id);
    }


    public async Task<IEnumerable<Service>> GetItems()
    {
        return await _servicesDbContext.Services.Include(x => x.ServiceCategory).ToListAsync();
    }


    public async Task<Service?> UpdateItem(Service updatedService)
    {
        Service? service = await _servicesDbContext.Services.FirstOrDefaultAsync(x => x.Id == updatedService.Id);

        if (service is not null)
        {
            service.Name = updatedService.Name;
            service.CategoryId = updatedService.CategoryId;
            service.Type = updatedService.Type;
            service.Description = updatedService.Description;
            service.Address = updatedService.Address;
            await SaveChangesAsync();
        }

        return service;
    }


    public async Task<Service?> DeleteItem(int id)
    {
        Service? service = await _servicesDbContext.Services.FirstOrDefaultAsync(x => x.Id == id);
        if (service is not null){
            _servicesDbContext.Services.Remove(service);
            await SaveChangesAsync();
        }
        
        return service;
    }


    private async Task SaveChangesAsync()
    {
        await _servicesDbContext.SaveChangesAsync();
    }
}
