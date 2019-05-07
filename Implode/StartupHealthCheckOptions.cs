using System;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Implode
{
    public class StartupHealthCheckOptions
    {
        public Func<HealthCheckRegistration, bool> Predicate { get; set; }
    }
}