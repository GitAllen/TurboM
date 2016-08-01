using System.Web.Optimization;

namespace Microsoft.Azure.CAT.Migration.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/lib").Include(
                        "~/wwwroot/lib/jquery/dist/jquery.js",
                        "~/wwwroot/lib/bootstrap/dist/js/bootstrap.js",
                        "~/wwwroot/lib/jquery-validation/dist/jquery.validate.js",
                        "~/wwwroot/lib/angular/angular.js"));

            bundles.Add(new ScriptBundle("~/bundles/cvf").Include(
                        "~/wwwroot/js/cvf/jquery.form.js",
                        "~/wwwroot/js/cvf/jquery.xml2json.js",
                        "~/wwwroot/js/cvf/cvf-1.0.core.js",
                        "~/wwwroot/js/cvf/cvf-1.0.window.js",
                        "~/wwwroot/js/cvf/cvf-1.0.dataRange.js",
                        "~/wwwroot/js/cvf/cvf-1.0.extension.js",
                        "~/wwwroot/js/cvf/cvf-1.0.gauges.js",
                        "~/wwwroot/js/cvf/cvf-1.0.nav.js",
                        "~/wwwroot/js/cvf/cvf-1.0.opie.js",
                        "~/wwwroot/js/cvf/cvf-1.0.start.js",
                        "~/wwwroot/js/cvf/cvf-1.0.utils.js",
                        "~/wwwroot/js/cvf/cvf-1.0.rules.js"));

            bundles.Add(new ScriptBundle("~/bundles/mtools").Include(
                       "~/wwwroot/js/site.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                       "~/wwwroot/lib/bootstrap/dist/css/bootstrap.css",
                       "~/wwwroot/lib/font-awesome/css/font-awesome.css",
                       "~/wwwroot/css/cvf.css",
                       "~/wwwroot/css/site.css"));
        }
    }
}
