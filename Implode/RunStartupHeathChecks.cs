using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;

namespace Implode
{
    public class RunStartupHeathChecks : IHostedService
    {
        private readonly HealthCheckService _healthCheckService;

        public RunStartupHeathChecks(HealthCheckService healthCheckService) => _healthCheckService = healthCheckService ?? throw new ArgumentNullException(nameof(healthCheckService));

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var report = await _healthCheckService.CheckHealthAsync();

            if (report.Status == HealthStatus.Unhealthy)
            {
                throw new StartupHealthCheckFailed();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
