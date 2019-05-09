using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Implode
{
    public class StartupHealthCheckFailed : System.Exception
    {
        public HealthReport HealthReport { get; set; }

        public StartupHealthCheckFailed(HealthReport healthReport)
        {
            this.HealthReport = healthReport;
        }
    }
}