using System.Web;
using System.Web.Mvc;
using JetBrains.Annotations;
using Microsoft.Azure.CAT.Migration.Common;
using Microsoft.Azure.CAT.Migration.Web.Auth;
using Microsoft.Azure.CAT.Migration.Web.Telemetry;
using Microsoft.Rest;

namespace Microsoft.Azure.CAT.Migration.Web.Controllers
{
    public class BaseController : Controller
    {
        protected readonly ITelemetryClient Telemetry;

        public BaseController(ITelemetryClient telemetry)
        {
            this.Telemetry = telemetry;
        }

        [CanBeNull]
        public TokenCredentials GetTokenCredentials(AzureEnvironment env)
        {
            HttpCookie cookie = HttpContext.GetArmTokenCookie(env);
            return cookie == null ? null : new TokenCredentials(cookie.Value);
        }

    }
}