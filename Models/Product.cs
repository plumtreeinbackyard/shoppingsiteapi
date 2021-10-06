using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace shoppingsiteapi.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Title")]
        public string Title { get; set; }

        public decimal Price { get; set; }

        public decimal Inventory { get; set; }

    }
}