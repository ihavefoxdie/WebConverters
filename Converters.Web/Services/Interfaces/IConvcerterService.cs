namespace Converters.Web.Services.Interfaces;

public interface IConvcerterService<T>
{
    public Task<T> SendData(int a, int b);
}
