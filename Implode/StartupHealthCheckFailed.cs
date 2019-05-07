namespace Implode
{
    public class StartupHealthCheckFailed : System.Exception
    {
        public StartupHealthCheckFailed() { }
        public StartupHealthCheckFailed(string message) : base(message) { }
        public StartupHealthCheckFailed(string message, System.Exception inner) : base(message, inner) { }
        protected StartupHealthCheckFailed(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}