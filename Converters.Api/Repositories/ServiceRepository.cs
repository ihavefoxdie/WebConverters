using Converters.Api.Repositories.Interfaces;
using Converters.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Converters.Api.Data;

namespace Converters.Api.Repositories;

//TODO: finish CRUD functionality
public class ServiceRepositry : IServiceRepository<Service>
{
    private readonly ServicesDbContext _servicesDbContext;

    public ServiceRepositry(ServicesDbContext servicesDbContext)
    {
        _servicesDbContext = servicesDbContext;
    }

    
    public async Task AddItem(string name, int categoryId, string type, string description, string address)
    {
        Service item = new Service{
            Name = name,
            CategoryId = categoryId,
            Type = type,
            Description = description,
            Address = address
        };
        await _servicesDbContext.Services.AddAsync(item);
        await _servicesDbContext.SaveChangesAsync();
    }

    public async Task<Service>? GetItem(int id)
    {
        return await _servicesDbContext.Services.Include(x => x.ServiceCategory).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Service>> GetItems()
    {
        return await _servicesDbContext.Services.Include(x => x.ServiceCategory).ToListAsync();
    }

    public async Task<ServiceCategory>? GetCategory(int id)
    {
        return await _servicesDbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<ServiceCategory>> GetCategories()
    {
        return await _servicesDbContext.Categories.ToListAsync();
    }

    //TODO: implement adding categories
    public Task AddCategory(string name)
    {
        throw new NotImplementedException();
    }

    //TODO: implement updating db items
    public Task UpdateItem(int id)
    {
        throw new NotImplementedException();
    }

    //TODO: implement updating categories
    public Task UpdateCategory(int id)
    {
        throw new NotImplementedException();
    }

    //TODO: implement deletetion of items
    public Task DeleteItem(int id)
    {
        throw new NotImplementedException();
    }

    //TODO: implement deletetion of categories
    public Task DeleteCategory(int id)
    {
        throw new NotImplementedException();
    }
}
