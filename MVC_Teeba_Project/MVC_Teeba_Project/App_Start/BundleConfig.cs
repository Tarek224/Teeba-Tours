using System.Web;
using System.Web.Optimization;

namespace MVC_Teeba_Project
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/css/animate.css",
                      "~/Content/css/icomoon.css",
                      "~/Content/css/bootstrap.css"));

            bundles.Add(new StyleBundle("~/Content/css/mine").Include(
                        "~/Content/css/style.css",
                        "~/Content/css/minestyle.css",
                        "~/Content/css/mystyle.css"));
        }
    }
}
