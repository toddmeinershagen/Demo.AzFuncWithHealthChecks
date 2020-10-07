using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;

namespace Demo.AzFuncWithHealthChecks.HealthChecks
{
    public static class HealthCheckServiceCollectionExtensions
    {
        public static IHealthChecksBuilder AddHealthChecks(this IServiceCollection services, bool useHealthCheckPublishers = true)
        {
            services.TryAddSingleton<HealthCheckService, DefaultHealthCheckService>();

            if (useHealthCheckPublishers)
            {
                services.TryAddEnumerable(ServiceDescriptor.Singleton<IHostedService, HealthCheckPublisherHostedService>());
            }

            return new HealthChecksBuilder(services);
        }

        public static IHealthChecksBuilder AddHealthChecksWithoutPublishers(this IServiceCollection services)
        {
            return AddHealthChecks(services, false);
        }
    }
}