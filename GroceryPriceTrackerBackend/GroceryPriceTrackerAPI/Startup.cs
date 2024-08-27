using GroceryPriceTrackerAPI.Data;
using GroceryPriceTrackerAPI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GroceryPriceTrackerAPI
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // Register MongoDBService
            services.AddSingleton<MongoDBService>();

            // Register GroceryScraper
            services.AddSingleton<GroceryScraper>();

            // Register HttpClient
            services.AddHttpClient();

            // Register the DataFetchingService as a hosted service
            services.AddHostedService<DataFetchingService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
