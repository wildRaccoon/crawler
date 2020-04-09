using System;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using crawler.lib.contracts;
using System.Reflection;

namespace crawler.console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var builder = new HostBuilder().ConfigureServices((HostBuilder, sc) =>
            {
                sc.AddHostedService<ConsoleHost>();

                var assembly = Assembly.GetAssembly(typeof(LinkData));

                sc.AddSingleton<IHostedService, ConsoleHost>();

                sc.Scan(s =>
                    s.FromAssemblies(assembly)
                        .AddClasses()
                        .AsImplementedInterfaces()
                        .WithSingletonLifetime());

                sc.Configure<MongoClientSettings>(opt =>
                {
                    opt.MinConnectionPoolSize = 1;
                    opt.MaxConnectionPoolSize = 25;
                    opt.Server = new MongoServerAddress("localhost",27017);
                });

                sc.AddSingleton<IMongoDatabase>(
                    sp => new MongoClient(sp.GetService<IOptions<MongoClientSettings>>().Value)
                            .GetDatabase("crawlrdb")
                );
            }).ConfigureLogging((hb, lb) =>
            {
                lb.AddConsole(cfg =>
                {
                    cfg.DisableColors = false;
                });
                lb.SetMinimumLevel(LogLevel.Trace);
            }).Build();

            await builder.RunAsync();
        }
    }
}
