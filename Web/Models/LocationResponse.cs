using System.Collections.Generic;

namespace Microsoft.Azure.CAT.Migration.Web.Models
{
    public class LocationResponse
    {
        public IEnumerable<Location> Locations { get; set; }  
    }
}