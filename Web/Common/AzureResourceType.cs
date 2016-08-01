using System;

namespace Microsoft.Azure.CAT.Migration.Web.Common
{
    public enum AzureResourceType
    {
        ResourceGroup,

        [HasDns(true)]
        StorageAccount,

        VirtualMachine,

        AvailabilitySet,

        NetworkInterfaceCard,

        VirtualNetwork,

        LoadBalancer,

        [HasDns]
        PublicIpAddress
    }
}