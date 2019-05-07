using System;
using System.Threading.Tasks;
using Implode;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Test
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var host = new HostBuilder()
                .ConfigureServices(services => {
                    services.AddHealthChecks();
                    services.AddHostedService<RunStartupHeathChecks>();
                })
                .Build();

            await host.RunAsync();
        }
    }
}
