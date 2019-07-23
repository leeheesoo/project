using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace KinderJoy.Site.Service
{
    public class RouteValuePresent : IRouteConstraint
    {
        public bool Match(HttpContextBase httpContext,
                          Route route, string parameterName,
                          RouteValueDictionary values,
                          RouteDirection routeDirection)
        {
            if (values.ContainsKey(parameterName))
            {
                return true;
            }
            return false;
        }
    }
}