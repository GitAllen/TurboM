using Microsoft.ApplicationInsights.DataContracts;

namespace Microsoft.Azure.CAT.Migration.Web.Telemetry
{
    public interface ITelemetryClient
    {
        TelemetryContext Context { get; }

        void TrackEvent(EventTelemetry ev);

        void TrackException(ExceptionTelemetry ex);
    }
}