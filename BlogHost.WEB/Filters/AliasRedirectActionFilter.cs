using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BlogHost.BLL.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BlogHost.WEB.Filters
{
    public class AliasRedirectActionFilter : Attribute, IActionFilter
    {
        private readonly IBlogService _blogService;

        public AliasRedirectActionFilter(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.RouteData.Values.ContainsKey("clientRoute"))
            {
                int? blodId = _blogService.GetBlogId(context.RouteData.Values["clientRoute"]?.ToString());
                if (blodId != null)
                {
                    context.ActionArguments["id"] = blodId;
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }
    }
}
