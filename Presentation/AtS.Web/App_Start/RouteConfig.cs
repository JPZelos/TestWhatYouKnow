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
                name: "Product",
                url: "catalog/product/{productId}",
                new { controller = "catalog", action = "ProductDetails" });

            //twyn
            routes.MapRoute(
                name: "Chapter",
                url: "catalog/chapter/{chapterId}",
                new { controller = "catalog", action = "ChaptertDetails" });

            //twyn
            routes.MapRoute(
                name: "DoTest",
                url: "catalog/DoTest/{chapterId}",
                new { controller = "answer", action = "DoTest" });


            //twyn
            routes.MapRoute(
                name: "ActionDenied",
                url: "catalog/ActionDenied",
                new { controller = "Admin", action = "ActionDenied" });

            //twyn
            routes.MapRoute(
                name: "AdminUsers",
                url: "admin/Users/",
                new { controller = "Admin", action = "AdminUsers" }
            );
            
            //twyn
            routes.MapRoute(
                name: "TeacherUsers",
                url: "admin/TeacherUsers",
                new { controller = "Admin", action = "TeacherUsers" }
            );

            //twyn
            routes.MapRoute(
                name: "TeacherTopics",
                url: "admin/TeacherTopics",
                new { controller = "Admin", action = "Topics" }
            );

            //twyn
            routes.MapRoute(
                name: "Chapters",
                url: "admin/Chapters",
                new { controller = "Admin", action = "Chapters" }
            );


            //twyn
            routes.MapRoute(
                name: "Questions",
                url: "admin/Questions/{chapterId}",
                new { controller = "Admin", action = "Questions" }
            );


            //admin home page
            routes.MapRoute(
                name: "AdminHomePage",
                url: "admin/",
                new { controller = "Admin", action = "Index" }
            );


            routes.MapRoute(
                name: "Login",
                url: "login/",
                new { controller = "Account", action = "Login" });


            //page not found
            routes.MapRoute(
                name: "PageNotFound",
                url: "page-not-found",
                new { controller = "Common", action = "PageNotFound" });


            routes.MapRoute(
                name: "DatabaseInfo",
                url: "page-not-found",
                new { controller = "Common", action = "DatabaseInfo" });


            //home page
            routes.MapRoute(
                name: "HomePage",
                url: "",
                new {controller = "Home", action = "Index"}
            );


            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new {controller = "Home", action = "Index", id = UrlParameter.Optional}
            );
        }
    }
}