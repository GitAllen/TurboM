using System;
using System.Web.Mvc;
using Microsoft.Azure.CAT.Migration.Web.Auth;
using Microsoft.Azure.CAT.Migration.Web.Telemetry;

namespace Microsoft.Azure.CAT.Migration.Web.Controllers
{
    [MvcAuthorize]
    public class HomeController : BaseController
    {
        public const string Name = "Home";

        public HomeController(ITelemetryClient telemtry) : base(telemtry)
        {
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
