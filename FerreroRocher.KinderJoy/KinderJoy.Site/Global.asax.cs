using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.WebPages;
using System.Web.Optimization;
using System.Data.Entity;
using KinderJoy.Domain.Infrastructure;
using ExpressiveAnnotations.Attributes;
using ExpressiveAnnotations.MvcUnobtrusive.Validators;
using KinderJoy.Site.App_Start;
using System.Web.Http;
using System.Web;
using System;
using System.Web.Helpers;

namespace KinderJoy.Site
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer<AppDbContext>(null);
            GlobalConfiguration.Configure(WebApiConfig.Register);

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AntiForgeryConfig.SuppressXFrameOptionsHeader = true;   // AntiForgeryToken 사용시 X-Frame-Options 중복 제거

            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(RequiredIfAttribute), typeof(RequiredIfValidator));
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(AssertThatAttribute), typeof(AssertThatValidator));

        }
        protected void Application_PreSendRequestHeaders(object sender, EventArgs e) {
            HttpContext.Current.Response.Headers.Remove("Server");
        }
    }
}
