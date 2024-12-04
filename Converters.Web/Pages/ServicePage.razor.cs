using Converters.Web.Services;
using Microsoft.AspNetCore.Components;

namespace Converters.Web.Pages
{
    public partial class ServicePage
    {
        [Parameter]
        public string? Name { get; set; }
        [Parameter]
        public int? ServiceId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            // if (Name is not null && ServiceAddress is not null)
            // {
            //     try
            //     {
            //         ConverterService converterService = new(new HttpClient{
            //         BaseAddress = new Uri(ServiceAddress)
            //     });
            //     }
            //     catch (Exception ex)
            //     {
            //         Console.WriteLine($"An error has occured:\n{ex.Message}");
            //     }
            // }
        }
    }
}