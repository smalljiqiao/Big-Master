using System.Web;
using System.Web.Optimization;

namespace BigMasterApp
{
    public class BundleConfig
    {
        // 有关捆绑的详细信息，请访问 https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/BaseJs").Include(
                     "~/Content/BaseJs/jquery.js",
                     "~/Content/BaseJs/layer.js",
                     "~/Content/BaseJs/vue2.js"));

            bundles.Add(new StyleBundle("~/bundle/BaseCss").Include(
                      "~/Content/BaseCss/layer.css"));
        }
    }
}
