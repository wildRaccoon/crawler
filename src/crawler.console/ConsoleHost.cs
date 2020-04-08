using System.Threading;
using System.Threading.Tasks;
using crawler.lib.services.Repository;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace crawler.console
{
    public class ConsoleHost : IHostedService
    {
        ILogger<ConsoleHost> logger;
        IDataRepository repository;
        public ConsoleHost(ILogger<ConsoleHost> logger, IDataRepository repository)
        {
            this.logger = logger;
            this.repository = repository;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("App Started");
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("App Stopped");
        }
    }
}