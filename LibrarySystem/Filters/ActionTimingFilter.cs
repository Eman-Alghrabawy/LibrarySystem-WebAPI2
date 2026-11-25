using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace LibrarySystem.Filters
{
    public class ActionTimingFilter(ILogger<ActionTimingFilter> logger) : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var stopwatch = Stopwatch.StartNew();

            try
            {
                await next();
            }
            finally
            {
                stopwatch.Stop();

                logger.LogInformation(
                    "Action {ActionName} took {ElapsedMillisconds}",
                    context.ActionDescriptor.DisplayName,
                    stopwatch.Elapsed.TotalMilliseconds);
            }
        }
    }
}
