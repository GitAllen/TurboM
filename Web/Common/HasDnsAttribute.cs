using System;

namespace Microsoft.Azure.CAT.Migration.Web.Common
{
    public class HasDnsAttribute : Attribute
    {
        public bool IsSameResourceNameRequired { get; }

        public HasDnsAttribute(bool isSameResourceNameRequired = false)
        {
            IsSameResourceNameRequired = isSameResourceNameRequired;
        }
    }
}