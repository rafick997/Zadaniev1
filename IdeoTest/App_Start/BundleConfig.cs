using System.Web;
using System.Web.Optimization;

namespace IdeoTest
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
         
            bundles.Add(new ScriptBundle("~/Content/woff2").Include(
                         "~/Content/Template/Layout/fonts/fontawesome-webfont.woff2"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"
                      ));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/Template/Layout/main.css",
                       "~/Content/Template/Layout/ie8.css",
                       "~/Content/Template/Layout/ie.css",
                       "~/Content/Template/Layout/font-awesome.min.css"
                      ));
        }
    }
}
