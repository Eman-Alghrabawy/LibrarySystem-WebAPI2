using Microsoft.AspNetCore.Mvc.Filters;

namespace LibrarySystem.Filters
{
    public class ActionExecutionLoggingFilter(ILogger<ActionExecutionLoggingFilter> logger) : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            logger.LogInformation("Starting action {ActionName}", context.ActionDescriptor.DisplayName);
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var actionName = context.ActionDescriptor.DisplayName;

            if (context.Exception is not null && !context.ExceptionHandled)
            {
                logger.LogError(context.Exception, "Action {actionName} failed.", actionName);
                return;
            }

            logger.LogInformation("Finishing action {ActionName}", actionName);
        }

       
    }
}
