using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Converters.Web;
using Converters.Web.Services.Interfaces;
using Converters.Models;
using Converters.Web.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7250/") });
builder.Services.AddScoped<IService<ServiceDTO>, MainService>();

await builder.Build().RunAsync();
