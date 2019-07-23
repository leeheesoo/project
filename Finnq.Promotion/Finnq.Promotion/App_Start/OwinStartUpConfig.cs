using Finnq.Promotion.Domain.Infrastructures;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Finnq.Promotion.App_Start {
    public class OwinStartUpConfig {
        public void Configuration(IAppBuilder app) {
            app.CreatePerOwinContext<AppDbContext>(AppDbContext.Create);
            app.CreatePerOwinContext<AppUserManager>(AppUserManager.Create);
            app.CreatePerOwinContext<AppRoleManager>(AppRoleManager.Create);

            app.UseCookieAuthentication(new CookieAuthenticationOptions {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/manager/login"),
                ExpireTimeSpan = TimeSpan.FromHours(24),
                SlidingExpiration = true
            });

        }
    }
}