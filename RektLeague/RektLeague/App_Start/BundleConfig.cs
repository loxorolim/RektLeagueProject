using System.Web;
using System.Web.Optimization;

namespace RektLeague
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/initial").Include(
                      "~/Scripts/MyScripts/SocialCalls.js",
                      "~/Scripts/jquery-{version}.js",
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/jquery.fittext.js")); 

            bundles.Add(new ScriptBundle("~/bundles/webposts").Include(
                      "~/Scripts/knockout-3.4.0.js",
                      "~/Scripts/MyScripts/WebPosts.js"));

            bundles.Add(new ScriptBundle("~/bundles/webpost").Include(
                      "~/Scripts/knockout-3.4.0.js",
                      "~/Scripts/MyScripts/WebPost.js"));

            bundles.Add(new ScriptBundle("~/bundles/webpostpartial").Include(
                      "~/Scripts/knockout-3.4.0.js",
                      "~/Scripts/MyScripts/WebPostPartial.js"));

            bundles.Add(new ScriptBundle("~/bundles/manageusers").Include(
                      "~/Scripts/knockout-3.4.0.js",
                      "~/Scripts/MyScripts/ManageUsers.js"));


            bundles.Add(new ScriptBundle("~/bundles/writepost").Include(
                      "~/Scripts/knockout-3.4.0.js",
                      "~/Scripts/jquery.validate*",
                      "~/Scripts/MyScripts/WritePost.js"));

            bundles.Add(new ScriptBundle("~/bundles/usersettings").Include(
                      "~/Scripts/jquery.validate*",
                      "~/Scripts/MyScripts/UserSettings.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootswatch/superhero/bootstrap.min.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/site.css",
                      "~/Content/WebPosts.css",
                      "~/Content/WebPostPartial.css"));
        }
    }
}
