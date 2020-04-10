using System.Collections.Generic;
using System.Threading.Tasks;
using crawler.lib.contracts;

namespace crawler.lib.services.Link
{
    public interface ILinkService
    {
        Task<PageData> DownloadAsync(LinkData link);

        Task<List<LinkData>> ParseLinksAsync(PageData page);
    }
}