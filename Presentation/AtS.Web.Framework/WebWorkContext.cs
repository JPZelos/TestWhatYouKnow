using System;
using System.Web;
using TWYK.Core;
using TWYK.Core.Domain;
using TWYK.Services.Authentication;
using TWYK.Services.Customers;

namespace TWYK.Web.Framework
{
    public class WebWorkContext : IWorkContext
    {
        #region Const

        private const string CustomerCookieName = "Ats.customer";

        #endregion

        #region Fields

        private readonly HttpContextBase _httpContext;
        private readonly ICustomerService _customerService;
        private readonly IAuthenticationService _authenticationService;

        private Customer _cachedCustomer;

        #endregion

        public WebWorkContext(
            HttpContextBase httpContext,
            ICustomerService customerService,
            IAuthenticationService authenticationService
        ) {
            _httpContext = httpContext;
            _customerService = customerService;
            _authenticationService = authenticationService;
        }

        protected virtual HttpCookie GetCustomerCookie() {
            if (_httpContext == null || _httpContext.Request == null) {
                return null;
            }

            return _httpContext.Request.Cookies[CustomerCookieName];
        }

        protected virtual void SetCustomerCookie(Guid customerGuid) {
            if (_httpContext != null && _httpContext.Response != null) {
                var cookie = new HttpCookie(CustomerCookieName);
                cookie.HttpOnly = true;
                cookie.Value = customerGuid.ToString();
                if (customerGuid == Guid.Empty) {
                    cookie.Expires = DateTime.Now.AddMonths(-1);
                }
                else {
                    int cookieExpires = 24 * 365; //TODO make configurable
                    cookie.Expires = DateTime.Now.AddHours(cookieExpires);
                }

                _httpContext.Response.Cookies.Remove(CustomerCookieName);
                _httpContext.Response.Cookies.Add(cookie);
            }
        }

        /// <summary>
        /// Gets or sets the current customer
        /// </summary>
        public Customer CurrentCustomer {
            get {
                if (_cachedCustomer != null) {
                    return _cachedCustomer;
                }

                Customer customer = null;

                //registered user
                customer = _authenticationService.GetAuthenticatedCustomer();

                //load guest customer
                if (customer == null) {
                    var customerCookie = GetCustomerCookie();
                    if (customerCookie != null && !string.IsNullOrEmpty(customerCookie.Value)) {
                        Guid customerGuid;
                        if (Guid.TryParse(customerCookie.Value, out customerGuid)) {
                            var customerByCookie = _customerService.GetCustomerByGuid(customerGuid);
                            if (customerByCookie != null &&
                                //this customer (from cookie) should not be registered
                                !customerByCookie.IsRegistered()) {
                                customer = customerByCookie;
                            }
                        }
                    }
                }

                //create guest if not exists
                if (customer == null) {
                    customer = _customerService.InsertGuestCustomer();
                }

                //validation
                //if (!customer.Deleted && customer.Active && !customer.RequireReLogin)
                SetCustomerCookie(customer.CustomerGuid);
                _cachedCustomer = customer;

                return _cachedCustomer;
            }
            set {
                SetCustomerCookie(value.CustomerGuid);
                _cachedCustomer = value;
            }
        }

        /// <summary>
        /// Get or set value indicating whether we're in admin area
        /// </summary>
        public virtual bool IsAdmin { get; set; }
    }
}