using System.Web.Mvc;
using System.Web.Routing;

namespace TWYK.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //define this routes to use in UI views (in case if you want to customize some of them later)
            routes.MapRoute(
                "Product",
                "catalog/product/{productId}",
                new { controller = "catalog", action = "ProductDetails" });

            //twyn
            routes.MapRoute(
                "Chapter",
                "catalog/chapter/{chapterId}",
                new { controller = "catalog", action = "ChaptertDetails" });

            //twyn
            routes.MapRoute(
                "DoTest",
                "catalog/DoTest/{chapterId}",
                new { controller = "answer", action = "DoTest" });

            //twyn
            routes.MapRoute(
                "ActionDenied",
                "catalog/ActionDenied",
                new { controller = "Admin", action = "ActionDenied" });

            //twyn
            routes.MapRoute(
                "AdminUsers",
                "admin/Users/",
                new { controller = "Admin", action = "AdminUsers" }
            );

            //twyn
            routes.MapRoute(
                "TeacherUsers",
                "admin/TeacherUsers",
                new { controller = "Admin", action = "TeacherUsers" }
            );

            //twyn
            routes.MapRoute(
                "TeacherTopics",
                "admin/TeacherTopics",
                new { controller = "Admin", action = "Topics" }
            );

            //twyn
            routes.MapRoute(
                "Chapters",
                "admin/Chapters",
                new { controller = "Admin", action = "Chapters" }
            );

            //twyn
            routes.MapRoute(
                "Questions",
                "admin/Questions/{chapterId}",
                new { controller = "Admin", action = "Questions" }
            );

            //admin home page
            routes.MapRoute(
                "AdminHomePage",
                "admin/",
                new { controller = "Admin", action = "Index" }
            );

            routes.MapRoute(
                "Login",
                "login/",
                new { controller = "Account", action = "Login" });

            //page not found
            routes.MapRoute(
                "PageNotFound",
                "page-not-found",
                new { controller = "Common", action = "PageNotFound" });

            routes.MapRoute(
                "DatabaseInfo",
                "page-not-found",
                new { controller = "Common", action = "DatabaseInfo" });

            //home page
            routes.MapRoute(
                "HomePage",
                "",
                new { controller = "Home", action = "Index" }
            );

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}