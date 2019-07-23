using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace KinderJoy.Site.Infrastructure {
    /// <summary>
    /// 모바일 기기로 접속 시 /m 붙여서 redirect
    /// </summary>
    public class RedirectToMobileAttribute  : ActionFilterAttribute {
        public override void OnActionExecuting(ActionExecutingContext filterContext) {
            string appPath = HttpContext.Current.Request.Path;

            string queryString = HttpContext.Current.Request.QueryString.ToString();
            string query = "";
            if (!string.IsNullOrEmpty(queryString)) {
                query = "/?" + HttpContext.Current.Request.QueryString.ToString();
            }

            if (filterContext.HttpContext.Request.Browser.IsMobileDevice) {
                if (!appPath.StartsWith("/m")) {
                    filterContext.Result = new RedirectResult("/m" + appPath + query);
                }
            }
            else {
                if (appPath.StartsWith("/m")) {
                    string url = appPath.Replace("/m", "");

                    filterContext.Result = new RedirectResult( (url=="" ? "/" : url) + query);
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }
}