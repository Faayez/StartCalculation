using Microsoft.Extensions.DependencyInjection;
using StartCalculation.Services.Repositories;
using System.Reflection;

namespace StartCalculation.Api.Infrastructure.Startup
{
    public static class StartupExtentions
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<ICalculationRepository, CalculationRepository>();
        }

        public static void RegisterMapping(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}
