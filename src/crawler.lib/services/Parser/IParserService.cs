using crawler.lib.contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace crawler.lib.services.Parser
{
    public interface IParserService
    {
        Task<List<string>> ResolveLinksAsync(PageData page);
    }
}
