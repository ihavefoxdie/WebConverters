using Converters.Api.Entities;
using Converters.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Converters.Api.Data;

namespace Converters.Api.Repositories;

public class CategoryRepository : ICategoryRepository<ServiceCategory>
{
    private readonly ServicesDbContext _servicesDbContext;

    public CategoryRepository(ServicesDbContext servicesDbContext)
    {
        _servicesDbContext = servicesDbContext;
    }

    public async Task AddCategory(ServiceCategory category)
    {
        await _servicesDbContext.Categories.AddAsync(category);
        await SaveChangesAsync();
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

    public async Task<ServiceCategory?> GetCategory(int id)
    {
        return await _servicesDbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<ServiceCategory>> GetCategories()
    {
        return await _servicesDbContext.Categories.ToListAsync();        
    }

    public async Task<ServiceCategory?> UpdateCategory(ServiceCategory category)
    {
        ServiceCategory? changingCategory = await _servicesDbContext.Categories.FirstOrDefaultAsync(x => x.Id == category.Id);

        if(changingCategory is not null)
        {
            changingCategory.Name = category.Name;
            changingCategory.Id = category.Id;
            await SaveChangesAsync();
        }

        return changingCategory;
    }

    private async Task SaveChangesAsync()
    {
        await _servicesDbContext.SaveChangesAsync();
    }
}
