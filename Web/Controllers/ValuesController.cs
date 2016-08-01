using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Azure.CAT.Migration.Web.Auth;
using Microsoft.Azure.CAT.Migration.Web.Common;
using Microsoft.Azure.CAT.Migration.Web.Telemetry;
using Microsoft.Azure.Management.Storage;
using Microsoft.Rest;

namespace Microsoft.Azure.CAT.Migration.Web.Controllers
{
    [ApiAuthorize]
    public class ValuesController : BaseApiController
    {
        public ValuesController(ITelemetryClient telemtry) : base(telemtry)
        {
        }

        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }

        [Route("api/environments/{env}/subscriptions/{subId}/sample")]
        public async Task<IHttpActionResult> SampleAsync(AzureEnvironment env, string subId)
        {
            TokenCredentials credentials = GetTokenCredentials(env);
            var client = new StorageManagementClient(credentials)
            {
                SubscriptionId = subId
            };

            using (var response = await client.StorageAccounts.CheckNameAvailabilityWithHttpMessagesAsync("test"))
            {
                return Ok(response.Body);
            }
        }
    }
}
