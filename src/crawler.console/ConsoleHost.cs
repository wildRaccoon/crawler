using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace crawler.console
{
    public class ConsoleHost : IHostedService
    {
        ILogger<ConsoleHost> logger;
        public ConsoleHost(ILogger<ConsoleHost> logger)
        {
            this.logger = logger;           
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