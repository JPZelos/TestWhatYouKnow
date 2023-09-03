using System.Collections.Generic;
using System.Linq;
using TWYK.Core.Domain;

namespace TWYK.Services.Orders
{
    /// <summary>
    /// Represents a shopping cart
    /// </summary>
    public static class ShoppingCartExtensions
    {
        /// <summary>
        /// Gets a number of product in the cart
        /// </summary>
        /// <param name="shoppingCart">Shopping cart</param>
        /// <returns>Result</returns>
        public static int GetTotalProducts(this IList<ShoppingCartItem> shoppingCart) {
            int result = 0;
            foreach (ShoppingCartItem sci in shoppingCart) {
                result += sci.Quantity;
            }

            return result;
        }

        /// <summary>
        /// Get customer of shopping cart
        /// </summary>
        /// <param name="shoppingCart">Shopping cart</param>
        /// <returns>Customer of shopping cart</returns>
        public static Customer GetCustomer(this IList<ShoppingCartItem> shoppingCart)
        {
            if (!shoppingCart.Any())
                return null;

            return shoppingCart[0].Customer;
        }
    }
}