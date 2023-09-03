using System;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TWYK.Core;
using TWYK.Core.Infrastructure;
using TWYK.Data;
using TWYK.Web.Controllers;

namespace TWYK.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start() {
            //most of API providers require TLS 1.2 nowadays
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            //disable "X-AspNetMvc-Version" header name
            MvcHandler.DisableMvcResponseHeader = true;

            //initialize engine context
            EngineContext.Initialize(false);

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_BeginRequest(object sender, EventArgs e) {
            //ignore static resources
            var webHelper = EngineContext.Current.Resolve<IWebHelper>();
            if (webHelper.IsStaticResource(Request)) {
                return;
            }

            //ensure database is installed
            if (!DataSettingsHelper.DatabaseIsInstalled()) {
                string installUrl = string.Format("{0}common/DatabaseInfo", webHelper.GetStoreLocation());
                if (!webHelper.GetThisPageUrl(false)
                        .StartsWith(installUrl, StringComparison.InvariantCultureIgnoreCase)) {
                    Response.Redirect(installUrl);
                }
            }
        }

        protected void Application_Error(Object sender, EventArgs e)
        {
            var exception = Server.GetLastError();

            //process 404 HTTP errors
            var httpException = exception as HttpException;
            if (httpException != null && httpException.GetHttpCode() == 404)
            {
                var webHelper = EngineContext.Current.Resolve<IWebHelper>();
                if (!webHelper.IsStaticResource(this.Request))
                {
                    Response.Clear();
                    Server.ClearError();
                    Response.TrySkipIisCustomErrors = true;

                    // Call target Controller and pass the routeData.
                    IController errorController = EngineContext.Current.Resolve<CommonController>();

                    var routeData = new RouteData();
                    routeData.Values.Add("controller", "Common");
                    routeData.Values.Add("action", "PageNotFound");

                    errorController.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));
                }
            }
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e) {
            //we don't do it in Application_BeginRequest because a user is not authenticated yet
            SetWorkingCulture();
        }

        protected void SetWorkingCulture() {
            if (!DataSettingsHelper.DatabaseIsInstalled()) {
                return;
            }

            //ignore static resources
            var webHelper = EngineContext.Current.Resolve<IWebHelper>();
            if (webHelper.IsStaticResource(Request)) {
                return;
            }

            //public store
            var workContext = EngineContext.Current.Resolve<IWorkContext>();
        }
    }
}