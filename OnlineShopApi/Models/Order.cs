using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace OnlineShopApi.Models
{
    public class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }

        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
        public OrderStatus Status { get; set; }
        public DateTime OrderDate { get; set; }
    }
    public class OrderItem
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string ProductId { get; set; }
        public int Quantity { get; set; }
    }

    public enum OrderStatus
    {
        Unknown,
        Placed,
        Shipped,
        Pending,
        Delivered,
        Canceled
    }
}
