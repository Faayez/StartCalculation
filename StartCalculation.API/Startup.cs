using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using StartCalculation.Api.Infrastructure.Startup;
using StartCalculation.Services;

namespace StartCalculation.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<CalculatorDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("CalculatorDb"));
            });

            services.RegisterMapping();

            services.RegisterServices();

            services.AddControllers();

            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc("CalculatorOpenAPISpecification", new OpenApiInfo
                {
                    Title = "Calculator Web API",
                    Version = "1"
                });
            });

            services.AddCors(opt => opt.AddDefaultPolicy(p =>
            {
                p.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithExposedHeaders("*");
            }));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(setupAction =>
                {
                    setupAction.SwaggerEndpoint("/swagger/CalculatorOpenAPISpecification/swagger.json", "Calculator API");
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}