using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogHost.WEB.Validators;

namespace BlogHost.WEB.RouteConstraints
{
    public class TitleConstraint : IRouteConstraint
    {
        public bool Match(HttpContext httpContext, IRouter route, string routeKey,
        RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (values.ContainsKey("clientRoute"))
            {
                return TextValidator.ValidateTitle(values["clientRoute"]?.ToString());
            }
            return false;
        }
    }
}
