using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using GroceryPriceTrackerAPI.Models;
using GroceryPriceTrackerAPI.Data;

namespace GroceryPriceTrackerAPI.Services
{
    public class DataFetchingService : IHostedService
    {
        private readonly GroceryScraper _scraper;
        private readonly MongoDBService _mongoService;
        private readonly ILogger<DataFetchingService> _logger;
        private Timer? _timer = null; // Initialize with null

        public DataFetchingService(GroceryScraper scraper, MongoDBService mongoService, ILogger<DataFetchingService> logger)
        {
            _scraper = scraper;
            _mongoService = mongoService;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("DataFetchingService is starting.");
            // Set the timer to run the scraping every hour
            _timer = new Timer(async state => await FetchAndStoreDataAsync(), null, TimeSpan.Zero, TimeSpan.FromHours(1));
            return Task.CompletedTask;
        }

        private async Task FetchAndStoreDataAsync()
        {
            _logger.LogInformation("Fetching data from Tesco...");
            var products = await _scraper.ScrapeTescoAsync();
            _logger.LogInformation($"Fetched {products.Count} products from Tesco.");

            await _mongoService.StoreProductsAsync(products);
            _logger.LogInformation("Stored products in MongoDB.");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("DataFetchingService is stopping.");
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }
    }
}
