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
        public IMongoCollection<SiteData> Sites { get; }

        public DataRepository(IMongoDatabase client)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));

            Pages = client.GetCollection<PageData>(nameof(PageData));
            Links = client.GetCollection<LinkData>(nameof(LinkData));
            Products = client.GetCollection<ProductData>(nameof(ProductData));
            Sites = client.GetCollection<SiteData>(nameof(SiteData));

            if (!Links.Indexes.List().Any())
            {
                Links.Indexes.CreateOne(new CreateIndexModel<LinkData>(Builders<LinkData>.IndexKeys.Ascending(_ => _.Url)));
            }

            if (Pages.Indexes.List().Any())
            {
                Pages.Indexes.CreateOne(new CreateIndexModel<PageData>(Builders<PageData>.IndexKeys.Ascending(_ => _.SiteId)));
                Pages.Indexes.CreateOne(new CreateIndexModel<PageData>(Builders<PageData>.IndexKeys.Ascending(_ => _.Url)));
            }

            if (Products.Indexes.List().Any())
            {
                Products.Indexes.CreateOne(new CreateIndexModel<ProductData>(Builders<ProductData>.IndexKeys.Ascending(_ => _.SiteId)));
                Products.Indexes.CreateOne(new CreateIndexModel<ProductData>(Builders<ProductData>.IndexKeys.Ascending(_ => _.Url)));
            }
        }
    }
}