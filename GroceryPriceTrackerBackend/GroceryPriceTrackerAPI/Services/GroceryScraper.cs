using HtmlAgilityPack;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using GroceryPriceTrackerAPI.Models;
using System.Net;

namespace GroceryPriceTrackerAPI.Services
{
    public class GroceryScraper
    {
        private static readonly HttpClientHandler handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator,
            UseCookies = true,
            CookieContainer = new CookieContainer()
        };

        private static readonly HttpClient client = new HttpClient(handler);

        public async Task<List<Product>> ScrapeTescoAsync()
        {
            var products = new List<Product>();
            var baseUrl = "https://www.tesco.com/groceries/en-GB/shop/marketplace/all"; // Base URL for the main site

            var request = new HttpRequestMessage(HttpMethod.Get, baseUrl);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("text/html"));
            request.Headers.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36");
            request.Headers.AcceptLanguage.Add(new StringWithQualityHeaderValue("en-US"));
            request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));
            request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("br"));

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var htmlContent = await response.Content.ReadAsStringAsync();
                var document = new HtmlDocument();
                document.LoadHtml(htmlContent);

                // Example of scraping categories
                var categoryNodes = document.DocumentNode.SelectNodes("//button[@aria-controls='filter-categories']");

                if (categoryNodes != null)
                {
                    foreach (var categoryNode in categoryNodes)
                    {
                        // Extract category URL and name
                        var categoryUrl = "https://www.tesco.com" + categoryNode.GetAttributeValue("href", "");
                        var categoryName = categoryNode.SelectSingleNode(".//span[@class='filter-select--label']").InnerText.Trim();

                        // Now scrape products in this category
                        var categoryProducts = await ScrapeCategoryProductsAsync(categoryUrl, categoryName);
                        products.AddRange(categoryProducts);
                    }
                }
                else
                {
                    // Log if no categories are found
                    Console.WriteLine("No category nodes found in the HTML response.");
                }
            }
            else
            {
                // Handle unsuccessful status codes (e.g., log the error, retry, etc.)
            }

            return products;
        }

        private async Task<List<Product>> ScrapeCategoryProductsAsync(string categoryUrl, string categoryName)
        {
            var products = new List<Product>();
            var response = await client.GetStringAsync(categoryUrl);
            var document = new HtmlDocument();
            document.LoadHtml(response);

            // Implement rate limiting
            await Task.Delay(2000); // 2-second delay between requests

            // Scrape product items
            var productNodes = document.DocumentNode.SelectNodes("//div[contains(@class, 'category product-list--page product-list--current-page')]");

            if (productNodes != null)
            {
                foreach (var node in productNodes)
                {
                    var name = node.SelectSingleNode(".//h3[contains(@class, 'product-title')]").InnerText.Trim();
                    var priceNode = node.SelectSingleNode(".//span[contains(@class, 'price')]");
                    var price = priceNode != null ? priceNode.InnerText.Trim() : "Price not available";
                    var imageUrl = node.SelectSingleNode(".//img").GetAttributeValue("src", "");
                    var stockNode = node.SelectSingleNode(".//div[contains(@class, 'stock')]");
                    var stock = stockNode != null ? stockNode.InnerText.Trim() : "In Stock";
                    var reviewsNode = node.SelectSingleNode(".//span[contains(@class, 'reviews')]");
                    var reviews = reviewsNode != null ? reviewsNode.InnerText.Trim() : "No reviews";

                    products.Add(new Product
                    {
                        Name = name,
                        Category = categoryName,
                        Price = price.Replace("£", "").Replace("p", "").Trim(),
                        ImageUrl = imageUrl,
                        Stock = stock,
                        Reviews = reviews,
                        Store = "Tesco"
                    });
                }
            }
            else
            {
                // Log if no product nodes are found
                Console.WriteLine("No product nodes found in the HTML response.");
            }

            return products;
        }
    }
}
