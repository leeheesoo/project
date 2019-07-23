using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Finnq.Promotion.Infrastructures {
    public class AccessIpAddressFilterAttribute : AuthorizeAttribute {
        public string[] AllowIpAddresses { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext) {
            bool isValid = true;

            if (httpContext == null)
                throw new ArgumentNullException("httpContext");

            // get current ip address
            var request = httpContext.Request;
            var ipAddress = request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? request.UserHostAddress;
            if (!AllowIpAddresses.Contains(ipAddress) && request.Url.Host.Equals("skg.finnq.com")) isValid = false;

            return isValid;
        }

    }
}