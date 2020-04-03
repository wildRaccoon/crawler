using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace crawler.lib.contracts
{
    public class ProductData
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Url { get; set; }
        public Dictionary<string, string> Props { get; set; }
    }
}