using System;
using System.Threading.Tasks;
using Implode.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
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

                    services.AddImplodeStartupHealthCheck();
                })
                .Build();

            await Assert.ThrowsAsync<StartupHealthCheckFailed>(() => host.StartAsync());
        }

        [Fact]
        public async Task Given_A_Passing_HealthCheck_When_The_Application_Starts_Up_Then_No_Exception_Is_Thrown()
        {
            var host = new HostBuilder()
                .ConfigureServices(services =>
                {
                    services.AddHealthChecks()
                        .AddCheck<PassingHealthCheck>("Passing HealthCheck");

                    services.AddImplodeStartupHealthCheck();
                })
                .Build();

            await host.StartAsync();
        }
    }
}
