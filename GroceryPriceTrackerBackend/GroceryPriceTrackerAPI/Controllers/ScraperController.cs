using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using GroceryPriceTrackerAPI.Data;
using GroceryPriceTrackerAPI.Models;
using System.Collections.Generic;
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

            // Deserialize JSON into a list of Product objects
            var products = JsonConvert.DeserializeObject<List<Product>>(result);

            // Store products in MongoDB
            await _mongoDBService.StoreProductsAsync(products);

            return Ok(products);
        }
        else
        {
            return StatusCode((int)response.StatusCode, response.ReasonPhrase);
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
