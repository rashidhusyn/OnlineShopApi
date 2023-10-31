using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace OnlineShopApi.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public int? Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }
    }
}
