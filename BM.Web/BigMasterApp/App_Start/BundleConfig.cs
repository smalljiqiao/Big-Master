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
                     "~/Content/BaseJs/vue2.js",
                     "~/Content/BaseJs/layer/layer.js",
                     "~/Content/BaseJs/FlexReset.js"));

            bundles.Add(new StyleBundle("~/bundles/BaseCss").Include(
                      "~/Content/BaseCss/reset.css",
                      "~/Content/BaseJs/layer/mobile/need/layer.css"));
        }
    }
}
