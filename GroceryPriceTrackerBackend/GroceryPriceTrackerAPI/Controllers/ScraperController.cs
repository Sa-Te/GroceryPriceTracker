
using Microsoft.AspNetCore.Mvc;

using GroceryPriceTrackerAPI.Data;
using GroceryPriceTrackerAPI.Models;

using Newtonsoft.Json;

[ApiController]
[Route("api/[controller]")]
public class ScraperController : ControllerBase
{
    private readonly HttpClient _httpClient;
    private readonly MongoDBService _mongoDBService;

    public ScraperController(HttpClient httpClient, MongoDBService mongoDBService)
    {
        _httpClient = httpClient;
        _mongoDBService = mongoDBService;
    }


    [HttpGet("search")]
    public async Task<IActionResult> Search(string query)
    {
        var response = await _httpClient.GetAsync($"http://localhost:8000/scrape?query={query}");

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Scraper result: " + result); // Log the raw JSON

            var products = JsonConvert.DeserializeObject<List<Product>>(result);

            if (products == null)
            {
                products = new List<Product>(); // Return an empty list if null
            }

            // Log the products being stored in MongoDB
            Console.WriteLine("Products to store: " + JsonConvert.SerializeObject(products));

            await _mongoDBService.StoreProductsAsync(products);

            return Ok(products);
        }
        else
        {
            return BadRequest(new { error = "Failed to fetch products", details = response.ReasonPhrase });
        }
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllProducts()
    {
        var products = await _mongoDBService.GetProductsAsync();
        return Ok(products);
    }

    [HttpGet("store/{storeName}")]
    public async Task<IActionResult> GetProductsByStore(string storeName)
    {
        var products = await _mongoDBService.GetProductsByStoreAsync(storeName);
        return Ok(products);
    }
}
