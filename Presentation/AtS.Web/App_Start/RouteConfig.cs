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
                name: "AdminTeacherUsers",
                url: "admin/TeacherUsers",
                new { controller = "Admin", action = "AdminTeacherUsers" }
            );

            //twyn
            routes.MapRoute(
                name: "TeacherTopics",
                url: "admin/TeacherTopics",
                new { controller = "Admin", action = "GetTecherTopics" }
            );

            //twyn
            routes.MapRoute(
                name: "TeacherChapters",
                url: "admin/TeacherChapters",
                new { controller = "Admin", action = "TeacherChapters" }
            );


            //twyn
            routes.MapRoute(
                name: "Questions",
                url: "admin/Questions/{chapterId}",
                new { controller = "Admin", action = "Questions" }
            );






            //shopping cart
            routes.MapRoute(
                name: "ShoppingCart",
                url: "cart/",
                new { controller = "ShoppingCart", action = "Cart" });

            //admin home page
            routes.MapRoute(
                name: "AdminHomePage",
                url: "admin/",
                new { controller = "Admin", action = "Index" }
            );

            routes.MapRoute(
                name: "AdminCategories",
                url: "admin/categories/",
                new { controller = "Admin", action = "Categories" }
            );
            routes.MapRoute(
                name: "AdminProducts",
                url: "admin/products/",
                new { controller = "Admin", action = "Products" }
            );

            //add product to cart (without any attributes and options). used on catalog pages.
            routes.MapRoute(
                name: "AddProductToCart-Catalog",
                url: "addproducttocart/{productId}/{quantity}",
                new {controller = "ShoppingCart", action = "AddProductToCart_Catalog"},
                new {productId = @"\d+", quantity = @"\d+"});

            //login page for checkout as guest
            routes.MapRoute(
                name: "Checkout",
                url: "Checkout/",
                new { controller = "ShoppingCart", action = "Checkout" });

            //login page for checkout as guest
            routes.MapRoute(
                name: "LoginCheckoutAsGuest",
                url: "login/checkoutasguest",
                new { controller = "Account", action = "Login", checkoutAsGuest = true });

            routes.MapRoute(
                name: "Login",
                url: "login/",
                new { controller = "Account", action = "Login" });

            //register result page

            routes.MapRoute(
                name: "Category",
                url: "category/{categoryId}",
                new { controller = "Catalog", action = "Category" },
                new { categoryId = @"\d+" });

            routes.MapRoute(
                name: "AllProducts",
                url: "products/",
                new { controller = "Catalog", action = "List" });

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