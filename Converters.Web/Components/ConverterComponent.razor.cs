using Microsoft.AspNetCore.Components;
using Converters.Models;
using Converters.Web.Services.Interfaces;
using Converters.Web.Services;

namespace Converters.Web.Components
{
    public partial class ConverterComponent
    {
        [Inject]
        private IService<ServiceDTO>? Service { get; set; }

        [Parameter]
        public string Name { get; set; }

        [Parameter]
        public int ServiceId { get; set; }

        private ServiceDTO ReturnedService { get; set; }
        private TestingInterpolation converterService { get; set; }

        private async void SendRequest()
        {
            double number = await converterService.SendData(30, 5);
            Console.WriteLine($"{number}");
        }

        protected override async Task OnInitializedAsync()
        {
            if (Service is not null)
            {

            }
        }

        protected override async Task OnParametersSetAsync()
        {
            if (Service is not null)
            {
                ReturnedService = await Service.GetItem(ServiceId);

                try
                {
                    converterService = new(new HttpClient
                    {
                        BaseAddress = new Uri("http://" + ReturnedService.Address + "/")
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error has occured:\n{ex.Message}");
                }

            }
        }
    }
}