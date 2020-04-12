using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace crawler.lib.contracts
{
    public class LinkData
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Url { get; set; }
        public bool Loaded { get; set; }
        public string Hash { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string SiteId { get; set; }
    }
}