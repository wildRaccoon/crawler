using System.Collections.Generic;
using System.Threading.Tasks;
using crawler.lib.contracts;

namespace crawler.lib.services.Link
{
    public class LinkService : ILinkService
    {
        public Task<PageData> DownloadAsync(LinkData link)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<LinkData>> ParseLinksAsync(PageData page)
        {
            throw new System.NotImplementedException();
        }
    }
}