using Converters.Api.Entities;

namespace Converters.Api.Repositories.Interfaces;

public interface IServiceRepository<T, T2>
{
    /// <summary>
    /// Add item to the database.
    /// </summary>
    /// <param name="name">Name of the item.</param>
    /// <param name="categoryId">ID of the corresponding category from the database.</param>
    /// <param name="type">Type of the item.</param>
    /// <param name="description">Description of the item.</param>
    /// <param name="address">Item's api address.</param>
    /// <returns>Status code of the operation.</returns>
    public Task AddItem(string name, int categoryId, string type, string description, string address);

    /// <summary>
    /// Add category to the database.
    /// </summary>
    /// <param name="name"></param>
    /// <returns>Status code of the operation.</returns>
    public Task AddCategory(string name);

    /// <summary>
    /// Get every item from the database.
    /// </summary>
    /// <returns>A collection of every item found in the database.</returns>
    public Task<IEnumerable<T>> GetItems();

    /// <summary>
    /// Get every item category from the database.
    /// </summary>
    /// <returns>A collection of every item category found in the database.</returns>
    public Task<IEnumerable<ServiceCategory>> GetCategories();

    /// <summary>
    /// Get item by id from database. 
    /// </summary>
    /// <param name="id">ID of the item.</param>
    /// <returns>Found item from the database.</returns>
    public Task<T?> GetItem (int id);

    /// <summary>
    /// Get item category by id from database.
    /// </summary>
    /// <param name="id">ID of the category</param>
    /// <returns>Found category from the database.</returns>
    public Task<T2?> GetCategory(int id);

    /// <summary>
    /// Edit particular item specified by its id.
    /// </summary>
    /// <param name="id">ID of the item.</param>
    /// <returns>Status code of the operation.</returns>
    public Task<T?> UpdateItem(int id, string name, int categoryId, string type, string description, string address);

    /// <summary>
    /// Edit particular category specified by its id.
    /// </summary>
    /// <param name="id">ID of the category.</param>
    /// <returns>Status code of the operation.</returns>
    public Task<T2?> UpdateCategory(int id, string name);
    
    /// <summary>
    /// Delete particular item specified by its id.
    /// </summary>
    /// <param name="id">ID of the item.</param>
    /// <returns>Status code of the operation.</returns>
    public Task<T?> DeleteItem(int id);

    /// <summary>
    /// Delete particular category specified by its id.
    /// </summary>
    /// <param name="id">ID of the category.</param>
    /// <returns>Status code of the operation.</returns>
    public Task<T2?> DeleteCategory(int id);
}
