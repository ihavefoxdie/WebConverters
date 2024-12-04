using Converters.Web.Services.Interfaces;
using Converters.Models;
using System.Net.Http.Json;

namespace Converters.Web.Services;

public class MainService : IService<ServiceDTO>
{
    private readonly HttpClient httpClient;

    public MainService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<ServiceDTO> GetItem(int id)
    {
        try
        {
            return await httpClient.GetFromJsonAsync<ServiceDTO>($"api/Service/GetService?id={id}");
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<IEnumerable<ServiceDTO>> GetItems()
    {
        try
        {
            return await httpClient.GetFromJsonAsync<IEnumerable<ServiceDTO>>("api/Service/GetServices");
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
}