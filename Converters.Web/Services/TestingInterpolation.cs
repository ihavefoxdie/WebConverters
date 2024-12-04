using Converters.Web.Services.Interfaces;
using System.Net.Http.Json;

namespace Converters.Web;

public class TestingInterpolation : IConvcerterService<double>
{
     private readonly HttpClient httpClient;

     public TestingInterpolation(HttpClient httpClient)
     {
        this.httpClient = httpClient;
     }

    public async Task<double> SendData(int a, int b)
    {
        try
        {
            return await httpClient.GetFromJsonAsync<double>($"/api/Interpolation/InterpolateXpow2?a={a}&b={b}");
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

}
