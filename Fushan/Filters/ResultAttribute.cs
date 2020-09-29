using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace Fushan.Filters
{
    public class ResultAttribute : IAsyncActionFilter
    {
        //public void OnActionExecuting(ActionExecutingContext context)
        //{
        //    // Do something before the action executes.
        //    MyDebug.Write(MethodBase.GetCurrentMethod(), context.HttpContext.Request.Path);
        //}

        async Task IAsyncActionFilter.OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultContext = await next();

            var result = resultContext.Result;
        }
    }
}