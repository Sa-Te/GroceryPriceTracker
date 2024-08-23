using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using GroceryPriceTrackerAPI.Data;
using GroceryPriceTrackerAPI.Models;
using GroceryPriceTrackerAPI.Services;

namespace GroceryPriceTrackerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly MongoDBService _mongoDBService;
        private readonly GroceryScraper _scraper;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(MongoDBService mongoDBService, GroceryScraper scraper, ILogger<ProductsController> logger)
        {
            _mongoDBService = mongoDBService;
            _scraper = scraper;
            _logger = logger;
        }

        [HttpPost("scrape/tesco")]
        public async Task<IActionResult> ScrapeTesco()
        {
            _logger.LogInformation("Starting Tesco scrape...");
            var products = await _scraper.ScrapeTescoAsync();
            _logger.LogInformation($"Scraped {products.Count} products from Tesco.");

            await _mongoDBService.StoreProductsAsync(products);
            _logger.LogInformation("Stored Tesco products in MongoDB.");

            return Ok(products);
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> GetProducts()
        {
            _logger.LogInformation("Fetching products from MongoDB.");
            return await _mongoDBService.GetProductsAsync();
        }
    }
}
