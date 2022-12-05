using System.Web;
using System.Web.Optimization;

namespace QLDanhMucKhoanThu
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                    "~/Content/bootstrap.css",
                    "~/Content/Site.css"));


            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            "~/Kendo/js/kendo.web.min.js"));
            //

            bundles.Add(new ScriptBundle("~/Kendo").Include("~/Kendo/js/kendo.all.min.js",
               "~/Kendo/js/kendo.aspnetmvc.min.js"));
            bundles.Add(new StyleBundle("~/Kendo/styles").Include("~/Kendo/styles/kendo.common.min.css",
                "~/Kendo/styles/kendo.default.min.css"));
        }
    }
}
