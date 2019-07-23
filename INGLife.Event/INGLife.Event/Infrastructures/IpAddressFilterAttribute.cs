using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace INGLife.Event.Infrastructures {
    public class IpAddressFilterAttribute : AuthorizeAttribute {
        public string[] AllowIpAddresses { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext) {
            bool isValid = false;

            if (httpContext == null)
                throw new ArgumentNullException("httpContext");

            // get current ip address
            var request = httpContext.Request;
            var ipAddress = request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? request.UserHostAddress;
            if (AllowIpAddresses.Contains(ipAddress)) {
                isValid = true;
            } else {
                httpContext.Response.Redirect("/error/not-found");
                httpContext.Response.End();
            }

            return isValid;
        }

    }
}