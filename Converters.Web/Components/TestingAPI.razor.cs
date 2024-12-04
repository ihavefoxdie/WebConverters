using Converters.Models;
using Converters.Web.Services.Interfaces;
using Microsoft.AspNetCore.Components;

namespace Converters.Web.Components
{
    public partial class TestingAPI
    {
        [Inject]
        public IService<ServiceDTO> Service { get; set; }
        [Parameter]
        public string? Name { get; set; }
        public IEnumerable<ServiceDTO> Services { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Services = await Service.GetItems();
        }
    }
}