using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Http;
using Microsoft.Azure.CAT.Migration.Common;
using Microsoft.Azure.CAT.Migration.Common.AzureManagement;
using Microsoft.Azure.CAT.Migration.Web.Models;
using Microsoft.Azure.CAT.Migration.Web.Telemetry;
using Microsoft.Azure.Management.ResourceManager;
using Microsoft.Rest;

namespace Microsoft.Azure.CAT.Migration.Web.Controllers
{
    public class AzureController : BaseApiController
    {
        private readonly IAzureManagementClientFactory factory;

        public AzureController(IAzureManagementClientFactory factory, ITelemetryClient telemtry) : base(telemtry)
        {
            this.factory = factory;
        }

        [Route("api/environments/{env}/subscriptions/{subId}/locations")]
        public async Task<IHttpActionResult> GetLocations(AzureEnvironment env, string subId)
        {
            string baseUri = WebConfigurationManager.AppSettings[env + ":ManagementBaseUri"];
            if (baseUri == null)
            {
                return NotFound();
            }

            TokenCredentials credentials = GetTokenCredentials(env);
            if (credentials == null)
            {
                return Unauthorized();
            }

            ISubscriptionClient client = factory.CreateSubscriptionClient(new Uri(baseUri), credentials);
            var locations = await client.Subscriptions.ListLocationsAsync(subId);
            var result = locations.Select(location => new Location
            {
                Name = location.Name,
                DisplayName = location.DisplayName
            });

            return Ok(new LocationResponse {Locations = result});
        }
    }
}
