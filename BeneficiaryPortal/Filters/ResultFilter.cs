using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeneficiaryPortal.Filters
{
    public class ResultFilter : IResultFilter
    {
        

        public void OnResultExecuted(ResultExecutedContext context)
        {
        }

       

        public void OnResultExecuting(ResultExecutingContext context)
        {
        }
    }
}
