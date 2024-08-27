using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GroceryPriceTrackerAPI.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Price { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string Stock { get; set; } = string.Empty;
        public string Reviews { get; set; } = string.Empty;
        public string Store { get; set; } = string.Empty;
    }
}
