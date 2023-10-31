using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace OnlineShopApi.Models
{
    public class Cart
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string UserId { get; set; }
        public List<CartItem> Items { get; set; } = new List<CartItem>();
    }
    public class CartItem
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
