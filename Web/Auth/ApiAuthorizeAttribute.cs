using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Microsoft.Azure.CAT.Migration.Web.Auth
{
    public class ApiAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var httpContext = new HttpContextWrapper(HttpContext.Current);
            HttpCookie authCookie = httpContext.GetAuthCookie();
            if (authCookie != null)
            {
                httpContext.SignInWithIdToken(authCookie.Value);
            }

            base.OnAuthorization(actionContext);
        }
    }
}