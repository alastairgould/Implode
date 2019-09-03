using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;

namespace Implode
{
    public class RunStartupHeathChecks : IHostedService
    {
        private const int HealthCheckFailedExitCode = 1;
        private readonly HealthCheckService _healthCheckService;
        private readonly IApplicationLifetime _applicationLifetime;

        public RunStartupHeathChecks(HealthCheckService healthCheckService, IApplicationLifetime applicationLifetime)
        {
            _healthCheckService = healthCheckService ?? throw new ArgumentNullException(nameof(healthCheckService));
            _applicationLifetime = applicationLifetime ?? throw new ArgumentNullException(nameof(applicationLifetime));
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var report = await _healthCheckService.CheckHealthAsync(cancellationToken);

            if (report.Status == HealthStatus.Unhealthy)
            {
                Environment.ExitCode = HealthCheckFailedExitCode;
                _applicationLifetime.StopApplication();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
