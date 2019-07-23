using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Optimization;
using ExpressiveAnnotations.Attributes;
using System.Web.Http;
using Finnq.Promotion.App_Start;
using ExpressiveAnnotations.MvcUnobtrusive.Validators;
using Elmah;
using System.Web;
using System;

namespace Finnq.Promotion {
    public class MvcApplication : System.Web.HttpApplication {
        protected void Application_Start() {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(RequiredIfAttribute), typeof(RequiredIfValidator));
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(AssertThatAttribute), typeof(AssertThatValidator));
        }
        protected void Application_PreSendRequestHeaders(object sender, EventArgs e) {
            HttpContext.Current.Response.Headers.Remove("Server");                        
        }
    }
}
