using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;

namespace HelloDotnets
{
    public class ExternalEndpointHealthCheck : IHealthCheck
    {
        private readonly WeatherSetting setting;

        public ExternalEndpointHealthCheck(IOptions<WeatherSetting> options)
        {
            this.setting = options.Value;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            Ping ping = new();
            var reply = await ping.SendPingAsync(this.setting.WeatherHost.Split('/')[0]);
            if (reply.Status != IPStatus.Success)
            {
                return HealthCheckResult.Unhealthy();
            }
            return HealthCheckResult.Healthy();
        }
    }
}