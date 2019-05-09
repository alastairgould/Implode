using System;
using System.Threading.Tasks;
using Implode.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace Implode.Tests
{
    public class ImplodeTests
    {
        [Fact]
        public async Task Given_A_Failing_HealthCheck_When_The_Application_Starts_Up_Then_A_StartupHealthCheckFailed_Exception_Is_Thrown()
        {
            var host = new HostBuilder()
                .ConfigureServices(services =>
                {
                    services.AddHealthChecks()
                        .AddCheck<FailingHealthCheck>("Failing HealthCheck");

                    services.AddImplodeOnStartupForUnhealthyHealthChecks();
                })
                .Build();

            var exception = await Assert.ThrowsAsync<StartupHealthCheckFailed>(() => host.StartAsync());

            Assert.Collection(exception.HealthReport.Entries, entry =>
            {
                Assert.Equal("Failing HealthCheck", entry.Key);
                Assert.Equal(HealthStatus.Unhealthy, entry.Value.Status);
            });
        }

        [Fact]
        public async Task Given_A_Passing_HealthCheck_When_The_Application_Starts_Up_Then_No_Exception_Is_Thrown()
        {
            var host = new HostBuilder()
                .ConfigureServices(services =>
                {
                    services.AddHealthChecks()
                        .AddCheck<PassingHealthCheck>("Passing HealthCheck");

                    services.AddImplodeOnStartupForUnhealthyHealthChecks();
                })
                .Build();

            await host.StartAsync();
        }
    }
}
