using Demo.AzFuncWithHealthChecks;
using Demo.AzFuncWithHealthChecks.HealthChecks;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Startup))]
namespace Demo.AzFuncWithHealthChecks
{
    
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var services = builder.Services;
            
            //option 1:  allow for hosted services to exist in a function
            //services.AddHealthChecks();

            //option 2:  add an additional parameter to signal no publishers
            //services.AddHealthChecks(false);

            //option 3:  add extension method with name signifying no publishers
            services
                .AddHealthChecksWithoutPublishers()
                .AddPrivateMemoryHealthCheck(1973741824);
        }
    }
}
