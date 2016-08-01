using System.Web.Configuration;
using Microsoft.Ajax.Utilities;
using Microsoft.Azure.CAT.Migration.Web.Common;

namespace Microsoft.Azure.CAT.Migration.Web.Auth
{
    public class SignInConfig
    {
        public static SignInConfig Create(AzureEnvironment env)
        {
            var config = new SignInConfig
            {
                ClientId = WebConfigurationManager.AppSettings[env + ":ClientId"],
                ClientSecret = WebConfigurationManager.AppSettings[env + ":ClientSecret"],
                Authority = WebConfigurationManager.AppSettings[env + ":Authority"],
                ArmResource = WebConfigurationManager.AppSettings[env + ":ArmResource"]
            };

            return (config.ClientId.IsNullOrWhiteSpace()
                    || config.ClientSecret.IsNullOrWhiteSpace()
                    || config.Authority.IsNullOrWhiteSpace()
                    || config.ArmResource.IsNullOrWhiteSpace())
                ? null : config;
        }

        private SignInConfig()
        {
        }

        public string ClientId { get; private set; }
        public string ClientSecret { get; private set; }
        public string Authority { get; private set; }
        public string ArmResource { get; private set; }
    }
}