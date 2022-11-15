using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Sigtatures.Web.Filters
{
    public class ValidationFilter<T> : ActionFilterAttribute
        where T : class
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var validator = context.HttpContext.RequestServices.GetService<IValidator<T>>();
            var modelPair = context.ActionArguments.FirstOrDefault(item => item.Value is T);
            if (modelPair.Value == null)
            {
                context.Result = new BadRequestObjectResult("Invalid validator configuration");
            }

            var model = modelPair.Value as T;
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                validationResult.Errors.ForEach(error =>
                {
                    context.ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                });

                context.Result = new BadRequestObjectResult(context.ModelState);
                return;
            }

            await base.OnActionExecutionAsync(context, next);
        }
    }
}
