using System.Linq;
using System.Web.Mvc;
using TWYK.Core;
using TWYK.Core.Domain;
using TWYK.Services.Orders;
using TWYK.Web.Models;

namespace TWYK.Web.Controllers
{
    public class CommonController : BaseController
    {
        private readonly IWorkContext _workContext;

        public CommonController(IWorkContext workContext) {
            _workContext = workContext;
        }
        
        public virtual ActionResult PageNotFound()
        {
            this.Response.StatusCode = 404;
            this.Response.TrySkipIisCustomErrors = true;
            this.Response.ContentType = "text/html";

            return View();
        }

        public ActionResult DatabaseInfo() {
            return View();
        }

        public virtual ActionResult AccessDenied()
        {
            this.Response.StatusCode = 403;
            this.Response.TrySkipIisCustomErrors = true;
            this.Response.ContentType = "text/html";

            return View();
        }

        // GET: Common
        [ChildActionOnly]
        public virtual ActionResult NavLinks()
        {
            var model = PrepareHeaderLinksModel();
            return PartialView(model);
        }

        private NavLinksModel PrepareHeaderLinksModel() {

            var customer = _workContext.CurrentCustomer;
            var alertMessage = string.Empty;

            var model = new NavLinksModel {
                IsAuthenticated = customer.IsRegistered()
            };

            //performance optimization (use "HasShoppingCartItems" property)
            if (customer.HasShoppingCartItems) {
                model.ShoppingCartItems = customer.ShoppingCartItems.ToList().GetTotalProducts();
            }

            return model;
        }
    }
}