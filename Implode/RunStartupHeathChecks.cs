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
        private readonly StartupHealthCheckOptions _startupHealthCheckOptions;

        public RunStartupHeathChecks(HealthCheckService healthCheckService, StartupHealthCheckOptions startupHealthCheckOptions) 
        {
            _healthCheckService = healthCheckService ?? throw new ArgumentNullException(nameof(healthCheckService));
            _startupHealthCheckOptions = startupHealthCheckOptions ?? throw new ArgumentNullException(nameof(startupHealthCheckOptions));
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var report = await _healthCheckService.CheckHealthAsync();

            if(report.Status == HealthStatus.Unhealthy)
            {
                throw new StartupHealthCheckFailed();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
