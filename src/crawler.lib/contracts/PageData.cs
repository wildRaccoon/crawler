using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace crawler.lib.contracts
{
    public class PageData
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Url { get; set; }
        public string Page { get; set; }
        public bool Analyzed { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string SiteId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string LinkId { get; set; }
    }
}