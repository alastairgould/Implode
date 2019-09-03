using System;
using System.Threading;
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
        public async Task Given_A_Failing_HealthCheck_When_The_Application_Starts_Up_Then_It_Is_Immediately_Shutdown()
        {
            var host = new HostBuilder()
                .ConfigureServices(services =>
                {
                    services.AddHealthChecks()
                        .AddCheck<FailingHealthCheck>("Failing HealthCheck");

                    services.AddImplodeOnStartupForUnhealthyHealthChecks();
                })
                .Build();

            await host.StartAsync();
            Assert.True(host.WaitForShutdownAsync().IsCompleted);
        }
        
        [Fact]
        public async Task Given_A_Failing_HealthCheck_When_The_Application_Starts_Up_Then_It_Immediately_Shutdown_With_Exit_Code_1()
        {
            Environment.ExitCode = 0;
            
            var host = new HostBuilder()
                .ConfigureServices(services =>
                {
                    services.AddHealthChecks()
                        .AddCheck<FailingHealthCheck>("Failing HealthCheck");

                    services.AddImplodeOnStartupForUnhealthyHealthChecks();
                })
                .Build();

            await host.StartAsync();
            Assert.Equal(1, Environment.ExitCode);
        } 

        [Fact]
        public async Task Given_A_Passing_HealthCheck_When_The_Application_Starts_Up_Then_It_Should_Not_Shutdown()
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
            
            Assert.False(host.WaitForShutdownAsync().IsCompleted);
        }
        
        [Fact]
        public async Task Given_A_Passing_HealthCheck_When_The_Application_Starts_Up_The_Exit_Code_Should_Still_Be_0()
        {
            Environment.ExitCode = 0;
            
            var host = new HostBuilder()
                .ConfigureServices(services =>
                {
                    services.AddHealthChecks()
                        .AddCheck<PassingHealthCheck>("Passing HealthCheck");

                    services.AddImplodeOnStartupForUnhealthyHealthChecks();
                })
                .Build();

            await host.StartAsync();
            
            Assert.Equal(0, Environment.ExitCode);
        }
    }
}
