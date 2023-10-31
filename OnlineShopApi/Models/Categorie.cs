using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace StoreAPI.Models
{
    public class Categorie
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string Title { get; set; } = null!;

        public string Descrtiption { get; set; } = null!;
       

    }
}
