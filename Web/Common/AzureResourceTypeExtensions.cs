using System;
using System.Linq;
using System.Reflection;

namespace Microsoft.Azure.CAT.Migration.Web.Common
{
    public static class AzureResourceTypeExtensions
    {
        private static HasDnsAttribute GetHasDnsAttribute(AzureResourceType resourceType)
        {
            Type type = typeof(AzureResourceType);
            FieldInfo memInfo = type.GetField(resourceType.ToString());
            var attributes = memInfo.GetCustomAttributes<HasDnsAttribute>(false);
            return attributes.FirstOrDefault();
        }

        public static bool HasDnsName(this AzureResourceType resourceType)
        {
            return GetHasDnsAttribute(resourceType) != null;
        }

        public static bool IsResourceNameConfigurable(this AzureResourceType resourceType)
        {
            HasDnsAttribute attribute = GetHasDnsAttribute(resourceType);
            return attribute == null || !attribute.IsSameResourceNameRequired;
        }
    }
}