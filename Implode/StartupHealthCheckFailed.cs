using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Implode
{
    public class StartupHealthCheckFailed : System.Exception
    {
        public HealthReport HealthReport { get; }

        public StartupHealthCheckFailed(HealthReport healthReport)
        {
            this.HealthReport = healthReport;
        }
    }
}