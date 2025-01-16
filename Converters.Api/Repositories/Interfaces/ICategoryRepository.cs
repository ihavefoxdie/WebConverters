
namespace Converters.Api.Repositories.Interfaces;

public interface ICategoryRepository<T>
{
    /// <summary>
    /// Adds an item (of type <c><typeparamref name="T"/></c>) to the database.
    /// </summary>
    /// <param name="category">Category object.</param>
    /// <returns>Task.</returns>
    public Task AddCategory(T category);

    /// <summary>
    /// Returns an item (of type <c><typeparamref name="T"/></c>) if it exists.
    /// </summary>
    /// <param name="id">ID of the item to return from the database.</param>
    /// <returns>Found item if found. Otherwise, returns null.</returns>
    public Task<T?> GetCategory(int id);

    /// <summary>
    /// Returns every item (of type <c><typeparamref name="T"/></c>) item from the database.
    /// </summary>
    /// <returns>A collection of every category item from the database.</returns>
    public Task<IEnumerable<T>> GetCategories();

    /// <summary>
    /// Updates the item (of type <c><typeparamref name="T"/></c>) with the matching ID.
    /// </summary>
    /// <param name="category">Category object to replace the older one with.</param>
    /// <returns>Updated category item.</returns>
    public Task<T?> UpdateCategory(T category);

    /// <summary>
    /// Deletes the item (of type <c><typeparamref name="T"/></c>) with the matching id.
    /// </summary>
    /// <param name="id">ID of the category to delete.</param>
    /// <returns>Deleted category item.</returns>
    public Task<T?> DeleteCategory(int id);
}
