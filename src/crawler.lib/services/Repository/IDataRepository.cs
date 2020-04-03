using crawler.lib.contracts;
using MongoDB.Driver;

namespace crawler.lib.services.Repository
{
    public interface IDataRepository
    {
        IMongoCollection<PageData> Pages { get; }
        IMongoCollection<LinkData> Links { get; }
        IMongoCollection<ProductData> Products { get; }
    }
}