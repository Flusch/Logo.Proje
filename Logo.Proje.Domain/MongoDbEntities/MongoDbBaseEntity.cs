using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Logo.Proje.Domain.MongoDbEntities
{
    public class MongoDbBaseEntity
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? LastUpdatedAt { get; set; }
        public string LastUpdatedBy { get; set; }
    }
}