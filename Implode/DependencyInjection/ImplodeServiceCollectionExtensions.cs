using Microsoft.Extensions.DependencyInjection;

namespace Implode.DependencyInjection
{
    public static class ImplodeServiceCollectionExtensions
    {
        public static void AddImplodeOnStartupForUnhealthyHealthChecks(this IServiceCollection serviceCollection) => serviceCollection.AddHostedService<RunStartupHeathChecks>();
    }
}