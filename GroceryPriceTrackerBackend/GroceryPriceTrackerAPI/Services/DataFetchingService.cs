using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace GroceryPriceTrackerAPI.Services
{
    public class DataFetchingService : IHostedService, IDisposable
    {
        private readonly ILogger<DataFetchingService> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private Timer? _timer; // Nullable Timer

        public DataFetchingService(ILogger<DataFetchingService> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("DataFetchingService is starting.");

            // Set the timer to run the scraping operation periodically, e.g., every hour (3600000 ms)
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromHours(1));

            return Task.CompletedTask;
        }

        private async void DoWork(object? state) // Nullable state parameter
        {
            _logger.LogInformation("DataFetchingService is fetching data.");

            try
            {
                var httpClient = _httpClientFactory.CreateClient();
                var response = await httpClient.GetAsync("http://localhost:8000/scrape?query=example");

                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Data fetched successfully from the scraper.");
                }
                else
                {
                    _logger.LogError($"Failed to fetch data. Status Code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while fetching data: {ex.Message}");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("DataFetchingService is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
