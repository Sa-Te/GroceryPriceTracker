using MongoDB.Driver;
using GroceryPriceTrackerAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroceryPriceTrackerAPI.Data
{
    public class MongoDBService
    {
        private readonly IMongoCollection<Product> _products;

        public MongoDBService()
        {
            var client = new MongoClient("mongodb+srv://akkisaitj:iZeS8lce0mfwMu4J@cluster0.ylgit.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0");
            var database = client.GetDatabase("GroceryDB");
            _products = database.GetCollection<Product>("Products");
        }

        public async Task StoreProductsAsync(List<Product> products)
        {
            if (products == null || !products.Any())
            {
                Console.WriteLine("No products to store.");  // Log when no products are found
                return;
            }

            Console.WriteLine("Inserting products to MongoDB...");  // Log insertion
            await _products.InsertManyAsync(products);
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            return await _products.Find(_ => true).ToListAsync();
        }

        public async Task<List<Product>> GetProductsByStoreAsync(string store)
        {
            var filter = Builders<Product>.Filter.Eq("Store", store);
            return await _products.Find(filter).ToListAsync();
        }
    }
}
