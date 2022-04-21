using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel;

namespace Logo.Proje.Domain.MongoDbEntities
{
    public class MongoDbBaseEntity
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public bool IsDeleted { get; set; }
        [DisplayName("Oluşturulma Tarihi")]
        public DateTime CreatedAt { get; set; }
        [DisplayName("Oluşturan")]
        public string CreatedBy { get; set; }
        [DisplayName("Son Düzenleme")]
        public DateTime? LastUpdatedAt { get; set; }
        [DisplayName("Son Düzenleyen")]
        public string LastUpdatedBy { get; set; }
    }
}