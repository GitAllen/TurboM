using System.Web;
using System.Web.Http;
using JetBrains.Annotations;
using Microsoft.Azure.CAT.Migration.Common;
using Microsoft.Azure.CAT.Migration.Web.Auth;
using Microsoft.Azure.CAT.Migration.Web.Telemetry;
using Microsoft.Rest;

namespace Microsoft.Azure.CAT.Migration.Web.Controllers
{
    public class BaseApiController : ApiController
    {
        protected readonly ITelemetryClient Telemetry;

        public BaseApiController(ITelemetryClient telemetry)
        {
            this.Telemetry = telemetry;
        }

        [CanBeNull]
        public TokenCredentials GetTokenCredentials(AzureEnvironment env)
        {
            HttpCookie cookie = new HttpContextWrapper(HttpContext.Current).GetArmTokenCookie(env);
            return cookie == null ? null : new TokenCredentials(cookie.Value);
        }
    }
}
