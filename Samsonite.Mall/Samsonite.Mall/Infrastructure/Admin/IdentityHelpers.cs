using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using Samsonite.Mall.Domain.Infrastructures;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;

namespace Samsonite.Mall.Infrastructure.Admin {
    public static class IdentityHelpers {
        public static MvcHtmlString GetName (this HtmlHelper html, string username) {
            AppUserManager mgr = HttpContext.Current.GetOwinContext().GetUserManager<AppUserManager>();
            return new MvcHtmlString(mgr.FindByNameAsync(username).Result.UserName);
        }

        public static MvcHtmlString GetRoleName (this HtmlHelper html, long id) {
            AppRoleManager mgr = HttpContext.Current.GetOwinContext().GetUserManager<AppRoleManager>();
            return new MvcHtmlString(mgr.FindByIdAsync(id).Result.Name);
        }

        public static MvcHtmlString GetUserNameAndName (this HtmlHelper html, long id) {
            AppUserManager mgr = HttpContext.Current.GetOwinContext().GetUserManager<AppUserManager>();
            var user = mgr.FindByIdAsync(id).Result;
            return new MvcHtmlString(string.Format("{0}", user.UserName));
        }


        public static bool IsInAnyRole (this IPrincipal principal, params string[] roles) {
            foreach (var role in roles) {
                if (principal.IsInRole(role)) {
                    return true;
                }
            }
            return false;
        }
    }
}