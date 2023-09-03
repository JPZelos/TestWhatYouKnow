using System;
using System.Collections.Generic;
using System.Linq;
using TWYK.Core.Data;
using TWYK.Core.Domain;
using TWYK.Services.Customers;
using TWYK.Services.Products;

namespace TWYK.Services.Orders
{
    public interface IShoppingCartService
    {
        /// <summary>
        /// Delete shopping cart item
        /// </summary>
        /// <param name="shoppingCartItem">Shopping cart item</param>
        void DeleteShoppingCartItem(ShoppingCartItem shoppingCartItem);

        /// <summary>
        /// Finds a shopping cart item in the cart
        /// </summary>
        /// <param name="shoppingCart">Shopping cart</param>
        /// <param name="product">Product</param>
        /// <returns>Found shopping cart item</returns>
        ShoppingCartItem FindShoppingCartItemInTheCart(IList<ShoppingCartItem> shoppingCart, Product product);

        /// <summary>
        /// Add a product to shopping cart
        /// </summary>
        /// <param name="customer">Customer</param>
        /// <param name="product">Product</param>
        /// <param name="quantity">Quantity</param>
        /// <returns>Warnings</returns>
        IList<string> AddToCart(Customer customer, Product product, int quantity = 1);

        /// <summary>
        /// Migrate shopping cart
        /// </summary>
        /// <param name="fromCustomer">From customer</param>
        /// <param name="toCustomer">To customer</param>
        /// <param name="includeCouponCodes">
        /// A value indicating whether to coupon codes (discount and gift card) should be also
        /// re-applied
        /// </param>
        void MigrateShoppingCart(Customer fromCustomer, Customer toCustomer, bool includeCouponCodes);

        /// <summary>
        /// Gets the total price of all shopping cart items
        /// </summary>
        /// <param name="cart">Shopping cart item</param>
        /// <returns>Total</returns>
        decimal GetShoppingCartSubTotal(IList<ShoppingCartItem> cart);

        /// <summary>
        /// Updates the shopping cart item
        /// </summary>
        /// <param name="customer">Customer</param>
        /// <param name="shoppingCartItemId">Shopping cart item identifier</param>
        /// <param name="quantity">New shopping cart item quantity</param>
        /// <returns>Warnings</returns>
        IList<string> UpdateShoppingCartItem(
            Customer customer,
            int shoppingCartItemId,
            int quantity = 1
        );
    }

    public class ShoppingCartService : IShoppingCartService
    {

        private readonly ICustomerService _customerService;
        private readonly IProductService _productService;
        private readonly IRepository<ShoppingCartItem> _sciRepository;

        public ShoppingCartService(
            ICustomerService customerService,
            IProductService productService,
            IRepository<ShoppingCartItem> sciRepository
        )
        {
            _customerService = customerService;
            _productService = productService;
            _sciRepository = sciRepository;
        }

        /// <summary>
        /// Delete shopping cart item
        /// </summary>
        /// <param name="shoppingCartItem">Shopping cart item</param>
        public virtual void DeleteShoppingCartItem(ShoppingCartItem shoppingCartItem) {
            if (shoppingCartItem == null) {
                throw new ArgumentNullException("shoppingCartItem");
            }

            var customer = shoppingCartItem.Customer;

            //delete item
            _sciRepository.Delete(shoppingCartItem);

            //reset "HasShoppingCartItems" property used for performance optimization
            customer.HasShoppingCartItems = customer.ShoppingCartItems.Any();
            _customerService.UpdateCustomer(customer);
        }

        /// <summary>
        /// Finds a shopping cart item in the cart
        /// </summary>
        /// <param name="shoppingCart">Shopping cart</param>
        /// <param name="product">Product</param>
        /// <returns>Found shopping cart item</returns>
        public virtual ShoppingCartItem FindShoppingCartItemInTheCart(
            IList<ShoppingCartItem> shoppingCart,
            Product product
        ) {
            if (shoppingCart == null) {
                throw new ArgumentNullException("shoppingCart");
            }

            if (product == null) {
                throw new ArgumentNullException("product");
            }

            foreach (var sci in shoppingCart) {
                if (sci.ProductId == product.Id) {
                    return sci;
                }
            }

            return null;
        }

        /// <summary>
        /// Add a product to shopping cart
        /// </summary>
        /// <param name="customer">Customer</param>
        /// <param name="product">Product</param>
        /// <param name="quantity">Quantity</param>
        /// <returns>Warnings</returns>
        public IList<string> AddToCart(Customer customer, Product product, int quantity = 1) {
            if (customer == null) {
                throw new ArgumentNullException("customer");
            }

            if (product == null) {
                throw new ArgumentNullException("product");
            }

            var warnings = new List<string>();

            if (quantity <= 0) {
                warnings.Add("Quantity Should be Positive.");
                return warnings;
            }

            var cart = customer.ShoppingCartItems.ToList();

            var shoppingCartItem = FindShoppingCartItemInTheCart(cart, product);
            if (shoppingCartItem != null) {
                //update existing shopping cart item
                int newQuantity = shoppingCartItem.Quantity + quantity;

                if (!warnings.Any()) {
                    shoppingCartItem.Quantity = newQuantity;
                    _customerService.UpdateCustomer(customer);
                }
            }
            else {
                if (!warnings.Any()) {
                    DateTime now = DateTime.UtcNow;
                    shoppingCartItem = new ShoppingCartItem {
                        Product = product,
                        Quantity = quantity,
                    };
                    customer.ShoppingCartItems.Add(shoppingCartItem);
                    _customerService.UpdateCustomer(customer);

                    //updated "HasShoppingCartItems" property used for performance optimization
                    customer.HasShoppingCartItems = customer.ShoppingCartItems.Any();
                    _customerService.UpdateCustomer(customer);
                }
            }

            return warnings;
        }

        /// <summary>
        /// Migrate shopping cart
        /// </summary>
        /// <param name="fromCustomer">From customer</param>
        /// <param name="toCustomer">To customer</param>
        /// <param name="includeCouponCodes">
        /// A value indicating whether to coupon codes (discount and gift card) should be also
        /// re-applied
        /// </param>
        public virtual void MigrateShoppingCart(Customer fromCustomer, Customer toCustomer, bool includeCouponCodes) {
            if (fromCustomer == null) {
                throw new ArgumentNullException("fromCustomer");
            }

            if (toCustomer == null) {
                throw new ArgumentNullException("toCustomer");
            }

            if (fromCustomer.Id == toCustomer.Id) {
                return; //the same customer
            }

            //shopping cart items
            var fromCart = fromCustomer.ShoppingCartItems.ToList();

            foreach (var sci in fromCart) {
                AddToCart(toCustomer, sci.Product, sci.Quantity);
            }

            for (int i = 0; i < fromCart.Count; i++) {
                var sci = fromCart[i];
                DeleteShoppingCartItem(sci);
            }
        }

        /// <summary>
        /// Gets the total price of all shopping cart items
        /// </summary>
        /// <param name="cart">Shopping cart item</param>
        /// <returns>Total</returns>
        public decimal  GetShoppingCartSubTotal(IList<ShoppingCartItem> cart) {
            if (!cart.Any())
                return 0;
            //get the customer 
            Customer customer = cart.GetCustomer();

            decimal subTotal = 0;
            foreach (var cartItem in cart) {
                var price = _productService.GetProductPrice(cartItem.ProductId);

                if (price > decimal.Zero) {
                    subTotal += price * cartItem.Quantity;
                }
            }

            //default round (Rounding001)
            var rez = Math.Round(subTotal, 2);

            return rez;
        }

        /// <summary>
        /// Updates the shopping cart item
        /// </summary>
        /// <param name="customer">Customer</param>
        /// <param name="shoppingCartItemId">Shopping cart item identifier</param>
        /// <param name="quantity">New shopping cart item quantity</param>
        /// <returns>Warnings</returns>
        public virtual IList<string> UpdateShoppingCartItem(
            Customer customer,
            int shoppingCartItemId,
            int quantity = 1
        ) {
            if (customer == null)
                throw new ArgumentNullException("customer");

            var warnings = new List<string>();

            var shoppingCartItem = customer.ShoppingCartItems.FirstOrDefault(sci => sci.Id == shoppingCartItemId);

            if (shoppingCartItem != null) {

                if (quantity > 0) {

                    //TODO: If in future must check product attributes, run checks here to populate warnings

                    if (!warnings.Any()) {
                        //if everything is OK, then update a shopping cart item
                        shoppingCartItem.Quantity = quantity;
                        _customerService.UpdateCustomer(customer);
                    }
                }
                else {
                    //delete a shopping cart item
                    DeleteShoppingCartItem(shoppingCartItem);
                }
            }

            return warnings;
        }

    }
}