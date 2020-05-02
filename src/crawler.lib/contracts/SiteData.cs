using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace crawler.lib.contracts
{
    public class SiteData
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Url { get; set; }
        public Dictionary<ePageQuery, string> HtmlQueries { get; set; }
    }
}
