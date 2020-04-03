using MongoDB.Driver;
using System;
using crawler.lib.contracts;

namespace crawler.lib.services.Repository
{
    public class DataRepository : IDataRepository
    {
        private IMongoDatabase client;

        public IMongoCollection<PageData> Pages { get; }
        public IMongoCollection<LinkData> Links { get; }
        public IMongoCollection<ProductData> Products { get; }

        public DataRepository(IMongoDatabase client)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));

            Pages = client.GetCollection<PageData>(nameof(PageData));
            Links = client.GetCollection<LinkData>(nameof(LinkData));
            Products = client.GetCollection<ProductData>(nameof(ProductData));

            if(!Links.Indexes.List().Any())
            {
                Links.Indexes.CreateOne(new CreateIndexModel<LinkData>(Builders<LinkData>.IndexKeys.Ascending(_ => _.Url)));
            }
        }
    }
}