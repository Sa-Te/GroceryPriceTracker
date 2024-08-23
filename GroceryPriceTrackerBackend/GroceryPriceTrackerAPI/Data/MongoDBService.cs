using MongoDB.Bson;
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
            // Use your actual MongoDB Atlas connection string
            var client = new MongoClient("mongodb+srv://akkisaitj:iZeS8lce0mfwMu4J@cluster0.ylgit.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0");
            var database = client.GetDatabase("GroceryDB");
            _products = database.GetCollection<Product>("Products");
        }

        public async Task StoreProductsAsync(List<Product> products)
        {
            if (products == null || products.Count == 0)
            {
                // Skip the operation if the list is empty
                return;
            }

            // Insert products into the collection
            await _products.InsertManyAsync(products);
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            // Retrieve all products from the collection
            return await _products.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<List<Product>> GetProductsByStoreAsync(string store)
        {
            // Retrieve products from a specific store
            var filter = Builders<Product>.Filter.Eq("Store", store);
            return await _products.Find(filter).ToListAsync();
        }
    }
}
