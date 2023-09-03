using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TWYK.Core;
using TWYK.Core.Domain;
using TWYK.Services.Orders;
using TWYK.Services.Products;
using TWYK.Web.Factories;
using TWYK.Web.Framework.Controllers;
using TWYK.Web.Infrastructure.Mapper;

namespace TWYK.Web.Controllers
{
    public class ShoppingCartController : BaseController
    {
        private readonly IWorkContext _workContext;
        private readonly IProductService _productService;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IShoppingCartModelFactory _shoppingCartModelFactory;

        public ShoppingCartController(
            IWorkContext workContext,
            IProductService productService,
            IShoppingCartService shoppingCartService,
            IShoppingCartModelFactory shoppingCartModelFactory
        ) {
            _workContext = workContext;
            _productService = productService;
            _shoppingCartService = shoppingCartService;
            _shoppingCartModelFactory = shoppingCartModelFactory;
        }

        [HttpPost]
        public virtual ActionResult AddProductToCart_Catalog(
            int productId,
            int quantity,
            bool forceredirection = false
        ) {
            var product = _productService.GetProductById(productId);

            //no product found
            if (product == null) {
                return Json(new {
                    success = false,
                    message = "No product found with the specified ID"
                });
            }

            //first, try to find existing shopping cart item
            var cart = _workContext.CurrentCustomer.ShoppingCartItems.ToList();
            var shoppingCartItem = _shoppingCartService.FindShoppingCartItemInTheCart(cart, product);

            //if we already have the same product in the cart, then use the total quantity to validate
            var quantityToValidate = shoppingCartItem != null ? shoppingCartItem.Quantity + quantity : quantity;

            //now let's try adding product to the cart
            var addToCartWarnings = _shoppingCartService.AddToCart(_workContext.CurrentCustomer, product, quantity);

            if (addToCartWarnings.Any()) {
                //cannot be added to the cart
                //let's do it on the product details page
                return Json(new {
                    redirect = Url.RouteUrl("Product", new {productId = product.Id}),
                });
            }

            var topCartTotalItems = _workContext.CurrentCustomer.ShoppingCartItems.ToList().GetTotalProducts();
            //display notification message and update appropriate blocks
            var updatetopcartsectionhtml = $"{topCartTotalItems}";

            var updateflyoutcartsectionhtml = RenderPartialViewToString("FlyoutShoppingCart",
                _shoppingCartModelFactory.PrepareShoppingCartModel());

            var scLink = Url.RouteUrl("ShoppingCart");
            return Json(new {
                success = true,
                message = $"The product has been added to your <a href=\"{scLink}\">shopping cart</a>",
                updatetopcartsectionhtml,
                updateflyoutcartsectionhtml
            });
        }

        public virtual ActionResult Cart() {
            var model = _shoppingCartModelFactory.PrepareShoppingCartModel();
            return View(model);
        }

        public virtual ActionResult FlyoutShoppingCart() {
            var model = _shoppingCartModelFactory.PrepareShoppingCartModel();
            return View(model);
        }

        [ValidateInput(false)]
        [HttpPost]
        [ActionName("Cart")]
        [FormValueRequired("updatecart")]
        public virtual ActionResult UpdateCart(FormCollection form) {
            var cart = _workContext.CurrentCustomer.ShoppingCartItems.ToList();

            var allIdsToRemove = form["removefromcart"] != null
                ? form["removefromcart"].Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToList()
                : new List<int>();

            //current warnings <cart item identifier, warnings>
            var innerWarnings = new Dictionary<int, IList<string>>();

            foreach (var sci in cart) {
                bool remove = allIdsToRemove.Contains(sci.Id);

                if (remove) {
                    _shoppingCartService.DeleteShoppingCartItem(sci);
                }
                else {
                    foreach (string formKey in form.AllKeys) {
                        if (!formKey.Equals($"itemquantity{sci.Id}",
                            StringComparison.InvariantCultureIgnoreCase)) {
                            continue;
                        }

                        int newQuantity;

                        //update cart with the new Quantity and get possible warnings
                        if (int.TryParse(form[formKey], out newQuantity)) {
                            var warnings = _shoppingCartService.UpdateShoppingCartItem(
                                _workContext.CurrentCustomer,
                                sci.Id,
                                newQuantity);

                            innerWarnings.Add(sci.Id, warnings);
                        }

                        break;
                    }
                }
            }

            //updated cart
            cart = _workContext.CurrentCustomer.ShoppingCartItems.ToList();
            var model = _shoppingCartModelFactory.PrepareShoppingCartModel(cart);

            //update current warnings
            foreach (var kvp in innerWarnings) {
                //kvp = <cart item identifier, warnings>
                var sciId = kvp.Key;
                var warnings = kvp.Value;
                //find model
                var sciModel = model.Items.FirstOrDefault(x => x.Id == sciId);

                if (sciModel != null) {
                    foreach (var w in warnings) {
                        if (!sciModel.Warnings.Contains(w)) {
                            sciModel.Warnings.Add(w);
                        }
                    }
                }
            }

            return View(model);
        }

        [ValidateInput(false)]
        [HttpPost]
        [ActionName("Cart")]
        [FormValueRequired("checkout")]
        public ActionResult StartCheckout() {
            var cart = _workContext.CurrentCustomer.ShoppingCartItems.ToList();
            //everything is OK
            if (_workContext.CurrentCustomer.IsGuest()) {
                return RedirectToRoute("LoginCheckoutAsGuest", new {returnUrl = Url.RouteUrl("Checkout")});
            }

            return RedirectToRoute("Checkout");
        }

        public ActionResult Checkout() {

            var model = _shoppingCartModelFactory.PrepareShoppingCartModel();
            _workContext.CurrentCustomer.HasShoppingCartItems = false;

            //clear customer shopping cart
            foreach (var sci in model.Items) {
                _shoppingCartService.UpdateShoppingCartItem(_workContext.CurrentCustomer, sci.Id, 0);
            }

            model.Customer = _workContext.CurrentCustomer.ToModel();

            return View(model);
        }
    }
}