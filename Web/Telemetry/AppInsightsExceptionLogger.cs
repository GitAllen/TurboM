using System.Web.Http.ExceptionHandling;
using Microsoft.ApplicationInsights;

namespace Microsoft.Azure.CAT.Migration.Web.Telemetry
{
    public class AppInsightsExceptionLogger : ExceptionLogger
    {
        private readonly TelemetryClient telemetry = new TelemetryClient();

        public override void Log(ExceptionLoggerContext context)
        {
            if (context?.Exception != null)
            {
                telemetry.TrackException(context.Exception);
            }
            base.Log(context);
        }
    }
}