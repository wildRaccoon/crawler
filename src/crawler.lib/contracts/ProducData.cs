using System.Collections.Generic;

namespace crawler.lib.contracts
{
    public class ProducData
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public Dictionary<string, string> Props { get; set; }
    }
}