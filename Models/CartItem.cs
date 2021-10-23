using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace shoppingsiteapi.Models
{
    public class CartItem
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public decimal Price { get; set; }

        public decimal Quantity { get; set; }
    }
}