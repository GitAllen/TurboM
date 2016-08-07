using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Ajax.Utilities;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.Azure.CAT.Migration.Common;
using Microsoft.Azure.CAT.Migration.Web.Auth;
using Microsoft.Azure.CAT.Migration.Web.Telemetry;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace Microsoft.Azure.CAT.Migration.Web.Controllers
{
    public class AuthController : BaseController
    {
        public const string Name = "Auth";

        public AuthController(ITelemetryClient telemtry) : base(telemtry)
        {
        }

        [Route("signin/{env}")]
        public ActionResult SignIn(AzureEnvironment env)
        {
            SignInConfig config = SignInConfig.Create(env);
            if (config == null)
            {
                return HttpNotFound();
            }

            const string nonce = "63567Dc4MDAw"; // TODO: this has to be properly generated in production!
            var routeValueDict = new RouteValueDictionary(new { env });
            string redirectUri = Server.UrlEncode(Url.Action(nameof(SignInCallback), Name, routeValueDict, Request.Url.Scheme));
            return Redirect($"{config.Authority}common/OAuth2/Authorize?client_id={config.ClientId}&response_mode=form_post&response_type=code+id_token&redirect_uri={redirectUri}&resource={config.ArmResource}&scope=openid+profile&nonce={nonce}");
        }

        [Route("signin/{env}/callback")]
        public async Task<ActionResult> SignInCallback(AzureEnvironment env, string code, string id_token, string error, string error_description)
        {
            if (code.IsNullOrWhiteSpace() || id_token.IsNullOrWhiteSpace())
            {
                return new HttpUnauthorizedResult(error_description ?? error ?? "Failed to sign in");
            }

            SignInConfig config = SignInConfig.Create(env);
            if (config == null)
            {
                return HttpNotFound();
            }

            ClientCredential credential = new ClientCredential(config.ClientId, config.ClientSecret);
            var routeValueDict = new RouteValueDictionary { { "env", env.ToString() } };
            var redirectUri = new Uri(Url.Action(nameof(SignInCallback), Name, routeValueDict, Request.Url.Scheme));
            AuthenticationContext authContext = new AuthenticationContext(config.Authority + "common/");
            AuthenticationResult result = await authContext.AcquireTokenByAuthorizationCodeAsync(code, redirectUri, credential);

            // Set cookies
            HttpContext.SetArmTokenCookie(env, result.AccessToken);
            if (env == AuthHelper.MainSiteEnvironment)
            {
                HttpContext.SetAuthCookie(id_token);
            }

            Telemetry.TrackEvent(new EventTelemetry("UserSignIn")
            {
                Properties =
                {
                    { "env", env.ToString() }
                }
            });

            return RedirectToAction("Index", HomeController.Name, null);
        }
    }
}