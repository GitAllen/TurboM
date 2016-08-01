using System.Web.Mvc;
using Microsoft.ApplicationInsights;

namespace Microsoft.Azure.CAT.Migration.Web.Telemetry
{
    public class AppInsightsHandleErrorAttribute : HandleErrorAttribute
    {
        private readonly TelemetryClient telemetry = new TelemetryClient();

        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext?.HttpContext != null && filterContext.Exception != null)
            {
                //If customError is Off, then AI HTTPModule will report the exception
                if (filterContext.HttpContext.IsCustomErrorEnabled)
                {
                    telemetry.TrackException(filterContext.Exception);
                }
            }
            base.OnException(filterContext);
        }
    }
}