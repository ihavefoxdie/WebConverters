using Converters.Api.Repositories.Interfaces;
using Converters.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Converters.Api.Data;
using System.ComponentModel;
using System.Reflection.Metadata.Ecma335;

namespace Converters.Api.Repositories;

//TODO: Add documentation
public class ServiceRepositry : IServiceRepository<Service, ServiceCategory>
{
    private readonly ServicesDbContext _servicesDbContext;

    public ServiceRepositry(ServicesDbContext servicesDbContext)
    {
        _servicesDbContext = servicesDbContext;
    }


    public async Task AddItem(string name, int categoryId, string type, string description, string address)
    {
        Service item = new Service
        {
            Name = name,
            CategoryId = categoryId,
            Type = type,
            Description = description,
            Address = address
        };
        await _servicesDbContext.Services.AddAsync(item);
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

    public async Task<ServiceCategory?> GetCategory(int id)
    {
        return await _servicesDbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<ServiceCategory>> GetCategories()
    {
        return await _servicesDbContext.Categories.ToListAsync();
    }

    public async Task AddCategory(string name)
    {
        await _servicesDbContext.Categories.AddAsync(new ServiceCategory
        {
            Name = name
        });
        await SaveChangesAsync();
    }

    public async Task<Service?> UpdateItem(int id, string name, int categoryId, string type, string description, string address)
    {
        Service? service = await _servicesDbContext.Services.FirstOrDefaultAsync(x => x.Id == id);

        if (service is not null)
        {
            service.Name = name;
            service.CategoryId = categoryId;
            service.Type = type;
            service.Description = description;
            service.Address = address;
            await SaveChangesAsync();
        }

        return service;
    }

    public async Task<ServiceCategory?> UpdateCategory(int id, string name)
    {
        ServiceCategory? category = await _servicesDbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);

        if (category is not null)
        {
            category.Name = name;
            await SaveChangesAsync();
        }

        return category;
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

    public async Task<ServiceCategory?> DeleteCategory(int id)
    {
        ServiceCategory? category = await _servicesDbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
        if (category is not null){
            _servicesDbContext.Categories.Remove(category);
            await SaveChangesAsync();
        }
        
        return category;
    }

    private async Task SaveChangesAsync()
    {
        await _servicesDbContext.SaveChangesAsync();
    }
}
