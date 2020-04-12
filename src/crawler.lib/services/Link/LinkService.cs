using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using crawler.lib.contracts;
using crawler.lib.services.Repository;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;
using System.Text;
using MongoDB.Driver;
using crawler.lib.services.Parser;
using System.Linq;

namespace crawler.lib.services.Link
{
    public class LinkService : ILinkService
    {
        IDataRepository repository;
        ILogger<LinkService> logger;
        WebClient webClient;
        HashSet<string> downloaded = new HashSet<string>();
        IParserService parser;

        public LinkService(IDataRepository repository, ILogger<LinkService> logger, IParserService parser, WebClient webClient)
        {
            this.repository = repository;
            this.logger = logger;
            this.webClient = webClient;
            this.parser = parser;
        }

        private string ComputeHash(string rawData)
        {
            using (var sha256Hash = SHA256.Create())
            {
                var bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                var builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private (string url, string hash) NormalizeLinkWithHash(string link)
        {
            var url = link.Trim().ToLower();
            var hash = ComputeHash(url);
            return (url, hash);
        }

        public async Task LoadSiteAsync(SiteData site)
        {
            var links = await repository.Links.FindAsync(f => f.SiteId == site.Id && f.Loaded);
            await links.ForEachAsync(ld => downloaded.Add(ld.Hash));
        }

        public async Task<PageData> DownloadAsync(LinkData link)
        {
            try
            {
                var content = await webClient.DownloadStringTaskAsync(link.Url);
                var page = new PageData()
                {
                    LinkId = link.Id,
                    Url = link.Url,
                    SiteId = link.SiteId,
                    Page = content
                };

                await repository.Pages.InsertOneAsync(page);
                downloaded.Add(link.Hash);

                return page;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Unable to download: {link.Id} - {link.Url}");
                return null;
            }
        }

        public async Task<List<LinkData>> ParseLinksAsync(PageData page)
        {
            var raw = await parser.ResolveLinksAsync(page);

            return raw.Select(x => NormalizeLinkWithHash(x))
                .Where(x => !downloaded.Contains(x.hash))
                .Select(x => new LinkData()
                {
                    SiteId = page.SiteId,
                    Url = x.url,
                    Hash = x.hash
                }).ToList();
        }
    }
}