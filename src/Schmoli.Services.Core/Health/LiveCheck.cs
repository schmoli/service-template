using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;

namespace Schmoli.Services.Core.Health
{
    /// <summary>
    /// Class used to inject as a standard health 'live' check.
    /// This check does nothing but return an OK if the service is 'up'
    /// </summary>
    public class LiveCheck : IHealthCheck
    {
        /// <summary>
        /// Perform a Liveness check. Does not verify dependencies (that's a ready check)
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>true if the service seems to be up and functioning, otherwise false</returns>
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            // TODO: do any other checks?
            var isLive = true;

            if (isLive)
            {
                return Task.FromResult(HealthCheckResult.Healthy("Service is live."));
            }

            return Task.FromResult(HealthCheckResult.Unhealthy("Service is not not live."));
        }
    }
}
