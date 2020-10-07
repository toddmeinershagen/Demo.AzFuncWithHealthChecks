using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Demo.AzFuncWithHealthChecks.HealthChecks
{
    /// <summary>
    /// This class is a mirror of the ASP.NET version because the original was marked internal
    /// </summary>
    internal class HealthChecksBuilder : IHealthChecksBuilder
    {
        public HealthChecksBuilder(IServiceCollection services)
        {
            Services = services;
        }

        public IServiceCollection Services { get; }

        public IHealthChecksBuilder Add(HealthCheckRegistration registration)
        {
            if (registration == null)
            {
                throw new ArgumentNullException(nameof(registration));
            }

            Services.Configure<HealthCheckServiceOptions>(options =>
            {
                options.Registrations.Add(registration);
            });

            return this;
        }
    }
}