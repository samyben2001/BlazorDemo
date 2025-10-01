using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace BlazorDemo
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            // besoin du NuGet 'Microsoft.Extensions.Http' pour injecter l'HttpClient dans les services
            builder.Services.AddHttpClient("api", options =>
            {
                options.BaseAddress = new Uri("https://localhost:7093/api/");
            });

            await builder.Build().RunAsync();
        }
    }
}
