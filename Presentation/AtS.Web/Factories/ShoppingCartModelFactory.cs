using System.Collections.Generic;
using System.Linq;
using TWYK.Core;
using TWYK.Core.Domain;
using TWYK.Services.Orders;
using TWYK.Web.Infrastructure.Mapper;
using TWYK.Web.Models;

namespace TWYK.Web.Factories
{
    public interface IShoppingCartModelFactory
    {
        ShoppingCartModel PrepareShoppingCartModel(IList<ShoppingCartItem> cart = null, ShoppingCartModel model = null);
    }

    public class ShoppingCartModelFactory : IShoppingCartModelFactory
    {
        private readonly IWorkContext _workContext;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IWebHelper _webHelper;

        public ShoppingCartModelFactory(IWorkContext workContext, IShoppingCartService shoppingCartService, IWebHelper webHelper) {
            _workContext = workContext;
            _shoppingCartService = shoppingCartService;
            _webHelper = webHelper;
        }

        public ShoppingCartModel PrepareShoppingCartModel(IList<ShoppingCartItem> cart = null, ShoppingCartModel model = null) {

             model ??= new ShoppingCartModel();

             cart ??= _workContext.CurrentCustomer.ShoppingCartItems.ToList();

            //performance optimization (use "HasShoppingCartItems" property)
            if (_workContext.CurrentCustomer.HasShoppingCartItems) {
               // var cart = _workContext.CurrentCustomer.ShoppingCartItems.ToList();
                model.Items = cart.ToModelList();
                model.TotalProducts = cart.GetTotalProducts();
                model.SubTotal = _shoppingCartService.GetShoppingCartSubTotal(cart);
            }

            model.Customer = _workContext.CurrentCustomer.ToModel();

            return model;
        }
    }
}