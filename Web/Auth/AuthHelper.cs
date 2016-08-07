using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.Web.Configuration;
using JetBrains.Annotations;
using Microsoft.Azure.CAT.Migration.Common;

namespace Microsoft.Azure.CAT.Migration.Web.Auth
{
    public static class AuthHelper
    {
        public const AzureEnvironment MainSiteEnvironment = AzureEnvironment.Global;
        public const string AuthenticationType = "Custom";
        public const string CookieName = "AuthCookie";
        public const string ArmTokenCookieNameSuffix = "Token";

        public static void SetSessionCookie(this HttpResponseBase response, string name, string value)
        {
            // This cookie expires when session ends
            var cookie = new HttpCookie(name, value)
            {
                // TODO: We must enforce HTTPS and set this flag before production
                // Secure = true,
            };
            response.SetCookie(cookie);
        }

        public static void ClearSessionCookie(this HttpResponseBase response, string name)
        {
            var cookie = new HttpCookie(name, string.Empty)
            {
                Expires = DateTime.Now.AddYears(-1)
            };
            response.SetCookie(cookie);
        }

        [CanBeNull]
        public static HttpCookie GetArmTokenCookie(this HttpContextBase httpContext, AzureEnvironment env)
        {
            return httpContext.Request.Cookies[env + ArmTokenCookieNameSuffix];
        }

        public static void SetArmTokenCookie(this HttpContextBase httpContext, AzureEnvironment env, string token)
        {
            httpContext.Response.SetSessionCookie(env + ArmTokenCookieNameSuffix, token);
        }

        [CanBeNull]
        public static HttpCookie GetAuthCookie(this HttpContextBase httpContext)
        {
            return httpContext.Request.Cookies[CookieName];
        }

        public static void SetAuthCookie(this HttpContextBase httpContext, string idToken)
        {
            httpContext.Response.SetSessionCookie(CookieName, idToken);
        }

        public static void ClearAuthCookie(this HttpContextBase httpContext)
        {
            httpContext.Response.ClearSessionCookie(CookieName);
        }

        public static bool SignInWithIdToken(this HttpContextBase httpContext, string idToken)
        {
            JwtSecurityToken token = new JwtSecurityToken(idToken);
            if (token.Audiences.FirstOrDefault() == WebConfigurationManager.AppSettings[MainSiteEnvironment + ":ClientId"]
                && token.ValidTo > DateTime.UtcNow)
            {
                string nameType = WebConfigurationManager.AppSettings["UserNameClaimType"];
                ClaimsIdentity identity = new ClaimsIdentity(token.Claims, AuthenticationType, nameType, null);
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                Thread.CurrentPrincipal = principal;
                httpContext.User = principal;

                return true;
            }

            return false;
        }
    }
}