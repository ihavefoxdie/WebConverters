namespace Converters.Web.Services.Interfaces;

public interface IService<T>
{
    public Task<IEnumerable<T>> GetItems();
    public Task<T> GetItem(int id);
}