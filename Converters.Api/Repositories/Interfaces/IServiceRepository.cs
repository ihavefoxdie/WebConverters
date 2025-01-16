using Converters.Api.Entities;

namespace Converters.Api.Repositories.Interfaces;

public interface IServiceRepository<T>
{
    /// <summary>
    /// Adds an item (of type <c><typeparamref name="T"/></c>) to the database.
    /// </summary>
    /// <param name="service">A new object to add.</param>
    /// <returns>Task.</returns>
    public Task AddItem(T service);


    /// <summary>
    /// Returns every item (of type <c><typeparamref name="T"/></c>) from the database.
    /// </summary>
    /// <returns>A collection of every item from the database.</returns>
    public Task<IEnumerable<T>> GetItems();


    /// <summary>
    /// Returns a item (of type <c><typeparamref name="T"/></c>) if it exists.
    /// </summary>
    /// <param name="id">ID of the item to return from the database.</param>
    /// <returns>Found item if found. Otherwise, returns null.</returns>
    public Task<T?> GetItem (int id);


    /// <summary>
    /// Updates the item (of type <c><typeparamref name="T"/></c>) with the matching ID.
    /// </summary>
    /// <param name="updatedService">The updated object to replace the older one with.</param>
    /// <returns>Updated item.</returns>
    public Task<T?> UpdateItem(T updatedService);

    
    /// <summary>
    /// Deletes the item (of type <c><typeparamref name="T"/></c>) with the matching id.
    /// </summary>
    /// <param name="id">ID of the item to delete.</param>
    /// <returns>Deleted item.</returns>
    public Task<T?> DeleteItem(int id);
}
