using AngleSharp.Html.Parser;
using System;
using System.IO;
using System.Linq;

namespace crawler.query
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("crawler.query [file] [agile query]");
                return;
            }

            var path = args.First();

            if (!File.Exists(path))
            {
                Console.WriteLine($"File {path} not found.");
                return;
            }

            var content = File.ReadAllText(path);

            var parsedHtml = new HtmlParser().ParseDocument(content);
            var result = parsedHtml.QuerySelectorAll(args.Last());

            Console.WriteLine($"Found {result.Count()}");

            if (result.Count() > 0)
            {
                foreach (var item in result)
                {
                    Console.WriteLine("--------------------------------------------------------");
                    Console.WriteLine(item.OuterHtml);
                    Console.WriteLine("--------------------------------------------------------");
                }
            }
        }
    }
}
