using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace StartCalculation.Wasm
{
    public class Program
    {
        public static Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.RootComponents.Add<App>("#app");

            //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:44350") });

            builder.Services.AddHttpClient("WebAPI", client => client.BaseAddress = new Uri("https://localhost:44350"));

            return builder.Build().RunAsync();
        }
    }
}
