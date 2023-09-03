﻿using System.Web.Mvc;
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