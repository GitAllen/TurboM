using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Azure.CAT.Migration.Web.Controllers;

namespace Microsoft.Azure.CAT.Migration.Web.Auth
{
    public class MvcAuthorizeAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = AuthController.Name,
                    action = "SignIn",
                    env = AuthHelper.MainSiteEnvironment
                }));
            }
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            HttpCookie authCookie = filterContext.HttpContext.GetAuthCookie();
            if (authCookie != null)
            {
                filterContext.HttpContext.SignInWithIdToken(authCookie.Value);
            }

            base.OnAuthorization(filterContext);
        }
    }
}