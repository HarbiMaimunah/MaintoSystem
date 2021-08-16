using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BeneficiaryPortal.Filters
{
    public class ActionFilter : IActionFilter
    {
        private readonly IConfiguration _config;
        public ActionFilter(IConfiguration configuration)
        {
            _config = configuration;
        }

        public void OnActionExecuting(ActionExecutingContext actionContext)
        {
            string path = _config.GetValue<string>("ActionPath");

            File.AppendAllText(path, "===================================Action Type===================================\n"
               + "Start Date :   " + DateTime.Now + "\n" +
                 actionContext.ActionDescriptor.RouteValues + "\n" +
                 actionContext.ActionDescriptor.DisplayName + "\n" +
                  "\n\n");
        }

        public void OnActionExecuted(ActionExecutedContext actionExecutedContext)
        {
        }
    }
}
