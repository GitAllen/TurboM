using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;

namespace Microsoft.Azure.CAT.Migration.Web.Telemetry
{
    public class TelemetryClientAdaptor : ITelemetryClient
    {
        private readonly TelemetryClient telemetry = new TelemetryClient();

        public TelemetryContext Context => telemetry.Context;

        public void TrackEvent(EventTelemetry ev)
        {
            telemetry.TrackEvent(ev);
        }

        public void TrackException(ExceptionTelemetry ex)
        {
            telemetry.TrackException(ex);
        }
    }
}