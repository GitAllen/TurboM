using System;
using System.Web.Configuration;

namespace Microsoft.Azure.CAT.Migration.Web.Common
{
    public static class Utils
    {
        public static Environment GetEnvironment()
        {
            Environment result;
            return Enum.TryParse(WebConfigurationManager.AppSettings["Environment"], true, out result)
                ? result : Environment.Local;
        }
    }
}