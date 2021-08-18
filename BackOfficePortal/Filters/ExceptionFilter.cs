using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BackOfficePortal.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly IConfiguration _config;
        public ExceptionFilter(IConfiguration configuration)
        {
            _config = configuration;
        }

        public void OnException(ExceptionContext context)
        {
            string path = _config.GetValue<string>("ExceptionPath");

           /* File.AppendAllText(path, "=====================================Error Logging ===================================\n\n" +
            "Start Date :   " + DateTime.Now +
            "\nError Message: \n" + context.Exception +
            "\n\nEnd Date :   " + DateTime.Now + "\n\n\n"
             );*/
        }
    }
}
