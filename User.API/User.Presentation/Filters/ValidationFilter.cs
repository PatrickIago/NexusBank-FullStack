using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace User.API.Filters;

public class ValidationFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var errors = context.ModelState
                .Where(x => x.Value.Errors.Any())
                .ToDictionary(
                    x => x.Key.ToLower(),
                    x => x.Value.Errors.First().ErrorMessage
                );

            context.Result = new BadRequestObjectResult(new
            {
                success = false,
                errors
            });
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {

    }
}