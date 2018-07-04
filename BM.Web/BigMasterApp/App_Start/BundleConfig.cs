using System.Web;
using System.Web.Optimization;

namespace BigMasterApp
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/BaseJs").Include(
                     "~/Content/BaseJs/jquery.js",
                     "~/Content/BaseJs/layer.js",
                     "~/Content/BaseJs/vue2.js"));

            bundles.Add(new StyleBundle("~/bundles/BaseCss").Include(
                      "~/Content/BaseCss/layer.css",
                      "~/Content/BaseCss/reset.css"));
        }
    }
}
