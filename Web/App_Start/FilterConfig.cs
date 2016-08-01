using System.Web.Mvc;
using Microsoft.Azure.CAT.Migration.Web.Telemetry;

namespace Microsoft.Azure.CAT.Migration.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new AppInsightsHandleErrorAttribute());
        }
    }
}
