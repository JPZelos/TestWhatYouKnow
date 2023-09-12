using System;
using System.Web.Mvc;
using TWYK.Core;
using TWYK.Core.Domain;
using TWYK.Services.Authentication;
using TWYK.Services.Customers;
using TWYK.Services.Orders;
using TWYK.Web.Framework.Controllers;
using TWYK.Web.Models;

namespace TWYK.Web.Controllers
{
    public class AccountController : BaseAdminController
    {
        #region Fields

        private readonly IAuthenticationService _authenticationService;
        private readonly ICustomerService _customerService;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IWorkContext _workContext;

        public AccountController(
            IAuthenticationService authenticationService,
            ICustomerService customerService,
            IShoppingCartService shoppingCartService,
            IWorkContext workContext
        ) {
            _authenticationService = authenticationService;
            _customerService = customerService;
            _shoppingCartService = shoppingCartService;
            _workContext = workContext;
        }

        #endregion

        #region Methods

        // GET: Account
        public ActionResult Login() {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model, string returnUrl) {
            if (ModelState.IsValid) {

                //add default administrator if not exist
               AddDefaultAdministrator();

                var loginResult = _customerService.ValidateCustomer(model.UserName.Trim(), model.Password);

                switch (loginResult) {
                    case CustomerLoginResults.Successful: {
                        var customer = _customerService.GetCustomerByUsername(model.UserName);

                        //migrate shopping cart
                        _shoppingCartService.MigrateShoppingCart(_workContext.CurrentCustomer, customer, true);

                        //sign in new customer
                        _authenticationService.SignIn(customer, true);

                        //sign in admin
                        if (
                            customer.RoleNames.Contains(SystemCustomerRoleNames.Administrators) ||
                            customer.RoleNames.Contains(SystemCustomerRoleNames.Teachers)
                        )
                        {
                            return RedirectToRoute("AdminHomePage");
                        }

                        if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl)) {
                            return RedirectToRoute("HomePage");
                        }

                        return Redirect(returnUrl);
                    }
                    case CustomerLoginResults.CustomerNotExist:
                        ModelState.AddModelError("", "Wrong Credentials Customer Not Exist");
                        break;

                    case CustomerLoginResults.NotRegistered:
                        ModelState.AddModelError("", "Wrong Credentials Not Registered");
                        break;

                    case CustomerLoginResults.LockedOut:
                        ModelState.AddModelError("", "Wrong Credentials LockedOut");
                        break;

                    case CustomerLoginResults.WrongPassword:
                    default:
                        ModelState.AddModelError("", "Wrong Credentials");
                        break;
                }
            }
            
            return View(model);
        }

        private void AddDefaultAdministrator_DD()
        {
            var customers = _customerService.GetAll();
            var newCustomer = new Customer
            {
                FirstName = "Jason",
                LastName = "Zelos Prapas",
                UserName = "admin",
                Email = "i.p.zelos@gmail.com",
                Password = "123",
                Address = "Agiou Spiridionos 45",
                Address2 = null,
                City = "Egaleo",
                State = "Attiki",
                Zip = "12243",
                RoleNames = "Registered,Administrators",
                IsAdmin = true,
                HasShoppingCartItems = false,
                LastLoginDateUtc = DateTime.UtcNow
            };

            if (customers == null)
            {
                _customerService.InsertCustomer(newCustomer);
            }
            else
            {
                string adminRole = SystemCustomerRoleNames.Administrators;
                bool adminExist = false;

                foreach (var c in customers)
                {
                    //if (c.RoleNames != null && c.RoleNames.Contains(adminRole)) {
                    //    adminExist = true;
                    //    break;
                    //}

                    if (c.IsInCustomerRole(SystemCustomerRoleNames.Administrators))
                    {
                        adminExist = true;
                        break;
                    }
                }

                if (!adminExist)
                {
                    _customerService.InsertCustomer(newCustomer);
                }
            }
        }

        private void AddDefaultAdministrator() {
            var customers = _customerService.GetAll();
            var newCustomer = new Customer {
                FirstName = "Jason",
                LastName = "Zelos Prapas",
                UserName = "admin",
                Email = "i.p.zelos@gmail.com",
                Password = "123",
                Address = "Agiou Spiridionos 45",
                Address2 = null,
                City = "Egaleo",
                State = "Attiki",
                Zip = "12243",
                RoleNames = "Registered,Administrators",
                IsAdmin = true,
                HasShoppingCartItems = false,
                LastLoginDateUtc = DateTime.UtcNow
            };

            if (customers == null) {
                _customerService.InsertCustomer(newCustomer);
            }
            else {
                string adminRole = SystemCustomerRoleNames.Administrators;
                bool adminExist = false;

                foreach (var c in customers) {
                    //if (c.RoleNames != null && c.RoleNames.Contains(adminRole)) {
                    //    adminExist = true;
                    //    break;
                    //}

                    if (c.IsInCustomerRole(SystemCustomerRoleNames.Administrators)) {
                        adminExist = true;
                        break;
                    }
                }

                if (!adminExist) {
                    _customerService.InsertCustomer(newCustomer);
                }
            }
        }

        /// <summary>
        /// https://stackoverflow.com/questions/3716153/how-to-remove-returnurl-from-url
        /// </summary>
        public ActionResult SignOut() {
            //standard logout 
            _authenticationService.SignOut();

            return RedirectToRoute("HomePage");
        }

        [ValidateInput(false)]
        [HttpPost]
        //[ActionName("Login")]
        [FormValueRequired("register")]
        public ActionResult Register(RegisterModel model, string returnUrl) {

            if (_workContext.CurrentCustomer.IsRegistered()) {
                //Already registered customer. 
                _authenticationService.SignOut();
                //Save a new record
                _workContext.CurrentCustomer = _customerService.InsertGuestCustomer();
            }

            var customer = _workContext.CurrentCustomer;
            var uniqueUserName = _customerService.UniqueUserName(model.UserName);
            if(!uniqueUserName)
                ModelState.AddModelError(model.UserName,"User Name already exists");

            if (ModelState.IsValid) {

                customer.LastLoginDateUtc = DateTime.UtcNow;
                customer.HasShoppingCartItems = _workContext.CurrentCustomer.HasShoppingCartItems;
                customer.IsAdmin = false;
                customer.RoleNames = "Registered,Students";
                customer.Zip = model.Zip;
                customer.State = model.State;
                customer.City = model.City;
                customer.Address2 = model.Address2;
                customer.Address = model.Address;
                customer.Password = model.Password;
                customer.UserName = model.UserName;
                customer.LastName = model.LastName;
                customer.FirstName = model.FirstName;

                //migrate shopping cart
                _shoppingCartService.MigrateShoppingCart(_workContext.CurrentCustomer, customer, true);

                //sign in new customer
                _authenticationService.SignIn(customer, true);

                _customerService.UpdateCustomer(customer);

                return RedirectToRoute(returnUrl);
            }

            return View("Login");
        }

        #endregion
    }
}