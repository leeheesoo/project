using System.Web;
using System.Web.Optimization;

namespace Samsonite.Mall {
    public class BundleConfig {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles) {
            bundles.Add(new StyleBundle("~/Content/commoncss").Include(
                      "~/Content/bootstrap*",
                      "~/Content/Site.css"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryform").Include(
                        "~/Scripts/jquery.form*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));


            bundles.Add(new ScriptBundle("~/bundles/jqueryunobtrusive").Include(
                "~/Scripts/jquery.unobtrusive-ajax.min.js",
                "~/Scripts/expressive.annotations.validate.js"
                ));
                        
            bundles.Add(new StyleBundle("~/Content/admincss").Include(
                "~/Content/metisMenu.min.css",
                "~/Content/sb-admin-2.css",
                "~/Content/font-awesome.min.css",
                "~/Content/css/admin/signup.css",
                "~/Content/css/admin-style.css"
                ));
            bundles.Add(new ScriptBundle("~/bundles/adminjs").Include(
                "~/Scripts/metisMenu.min.js",
                "~/Scripts/sb-admin-2.js",
                "~/Scripts/bootstrap-datepicker.js",
                "~/Scripts/locales/bootstrap-datepicker.kr.min.js",
                "~/Scripts/jquery.fancybox.pack.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/commonjs").Include(
                "~/Scripts/moment.*",
                "~/Content/js/common/common.js"));

            bundles.Add(new ScriptBundle("~/bundles/knockoutjs").Include(
                "~/Scripts/knockout*"
                //"~/Scripts/knockout.mapping-latest"
                ));
        }
    }
}