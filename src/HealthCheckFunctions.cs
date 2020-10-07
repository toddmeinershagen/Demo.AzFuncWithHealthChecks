using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;

namespace Demo.AzFuncWithHealthChecks
{
    public class HealthCheckFunctions
    {
        private readonly ILogger _logger;
        private readonly HealthCheckService _healthCheckService;

        public HealthCheckFunctions(ILogger<HealthCheckFunctions> logger, HealthCheckService healthCheckService)
        {
            _logger = logger;
            _healthCheckService = healthCheckService;
        }

        [FunctionName(nameof(GetHealth))]
        public async Task<IActionResult> GetHealth(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "health")]
            HttpRequest req,
            CancellationToken cancelToken)
        {
            var report = await _healthCheckService.CheckHealthAsync(cancelToken);
            var response = new HealthReportResponse(report);

            var result = report.Status == HealthStatus.Healthy || report.Status == HealthStatus.Degraded
                ? new OkObjectResult(response)
                : new ObjectResult(response) { StatusCode = (int)HttpStatusCode.ServiceUnavailable };

            return await Task.FromResult(result);
        }
    }
}