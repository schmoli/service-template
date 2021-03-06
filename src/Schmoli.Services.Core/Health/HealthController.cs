using System;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Schmoli.Services.Core.Health
{
    /*
    Can be done direclty in configure:
                app.UseHealthChecks("/health",
                    new HealthCheckOptions
                    {
                        ResponseWriter = async (context, report) =>
                        {
                            var result = JsonSerializer.Serialize(
                                new {
                                    status = report.Status.ToString(),
                                    errors = report.Entries.Select(e => new { key = e.Key, value = Enum.GetName(typeof(HealthStatus), e.Value.Status) })
                                }
                            );
                            context.Response.ContentType = MediaTypeNames.Application.Json;
                            await context.Response.WriteAsync(result);
                        }
                    });
    */
    [Route("/health")]
    public class HealthController : ControllerBase
    {
        private readonly HealthCheckService _healthCheckService;
        public HealthController(HealthCheckService healthCheckService)
        {
            _healthCheckService = healthCheckService;
        }

        /// <summary>
        /// Execute all health checks and return a detailed report.
        /// </summary>
        /// <remarks>Provides an indication about the health of the API</remarks>
        /// <response code="200">API is healthy</response>
        /// <response code="503">API is unhealthy or in degraded state</response>
        [HttpGet]
        public async Task<IActionResult> GetHealthReport()
        {
            HealthReport report = await _healthCheckService.CheckHealthAsync();

            var result = JsonSerializer.Serialize(
                            new
                            {
                                status = report.Status.ToString(),
                                errors = report.Entries.Select(e => new { key = e.Key, value = Enum.GetName(typeof(HealthStatus), e.Value.Status) })
                            }, options: new JsonSerializerOptions { WriteIndented = true });

            return report.Status == HealthStatus.Healthy ? Ok(result) : StatusCode((int)HttpStatusCode.ServiceUnavailable, result);
        }

        /// <summary>
        /// Determine if API is ready to receive traffic.
        /// </summary>
        /// <remarks>Returns services ability to recieve traffic by testing the readiness of all dependent services</remarks>
        /// <response code="200">API is ready to receive traffic</response>
        /// <response code="503">API is unhealthy or in degraded state</response>
        /// <returns></returns>
        [HttpGet("ready")]
        public Task<IActionResult> Ready()
        {
            // Implmented in Startup, only here for Swagger Generation
            throw new NotImplementedException();
        }

        /// <summary>
        /// Determine if service is live (started but might not be ready).
        /// </summary>
        /// <remarks>To be used by kubernetes as a liveness check</remarks>
        /// <response code="200">Service is live</response>
        /// <response code="503">Service is not live</response>
        /// <returns></returns>
        [HttpGet("live")]
        public Task<IActionResult> Live()
        {
            // Implmented in Startup, only here for Swagger Generation
            throw new NotImplementedException();
        }
    }
}
