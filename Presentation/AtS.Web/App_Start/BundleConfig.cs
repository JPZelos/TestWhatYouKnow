using System.Collections.Generic;
using System.IO;
using System.Web.Optimization;

namespace TWYK.Web
{
    /// <summary>
    /// see: https://stackoverflow.com/questions/19323409/asp-net-mvc-bundle-config-order
    /// </summary>
    public class NonOrderingBundleOrderer : IBundleOrderer
    {
        public IEnumerable<FileInfo> OrderFiles(BundleContext context, IEnumerable<FileInfo> files)
        {
            return files;
        }

        #region Implementation of IBundleOrderer

        public IEnumerable<BundleFile> OrderFiles(BundleContext context, IEnumerable<BundleFile> files) {
            return files;
        }

        #endregion
    }

    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            var mitsos = (new Bundle("~/bundles/mitsos").Include(
                "~/Content/assets/js/Bootstrap/bootstrap.min.js",
                "~/Content/assets/js/jQuery/jquery-3.6.3.min.js"
            ));

            //see: https://stackoverflow.com/questions/19323409/asp-net-mvc-bundle-config-order
            mitsos.Orderer = new NonOrderingBundleOrderer();
            bundles.Add(mitsos);

            bundles.Add(new ScriptBundle("~/bundles/ajaxcart").Include(
                        "~/Content/js/custom/public.common.js",
                        "~/Content/js/custom/public.ajaxcart.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Content/js/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Content/js/modernizr-*"));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/assets/css/Bootstrap/bootstrap.min.css",
                "~/Content/assets/css/style.css",
                "~/Content/assets/css/responsive.css",
                "~/Content/assets/css/animation.css",
                "~/Content/assets/css/result_style.css"));

        }
    }
}
