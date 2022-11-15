using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Signarutes.Domain.Contracts.Exceptions;
using System.Net;

namespace Sigtatures.Web.Filters
{
    public class ErrorHandlerFilter : IExceptionFilter
    {
        // this is just an error handling example
        // in a real life this could be filter/middleware
        // some basic logging would be here
        // we would decide on what to highlight/hide (based on env. most likely)
        // etc.
        public void OnException(ExceptionContext context)
        {
            var logger = GetLogger(context);
            var baseException = context.Exception.GetBaseException();

            logger.LogError(context.Exception, context.Exception.Message);


            if (context.Exception is DomainException)
            {
                context.Result = new BadRequestObjectResult(context.Exception.Message);
                return;
            }

            if (baseException is DomainException)
            {
                context.Result = new BadRequestObjectResult(baseException.Message);
                return;
            }


            context.Result = new ObjectResult("Internal server error")
            {
                StatusCode = (int)HttpStatusCode.InternalServerError
            };
        }

        private ILogger GetLogger(ExceptionContext context)
        {
            string name = (string)context.RouteData.Values["Controller"];
            var loggerFactory = context.HttpContext.RequestServices.GetService<ILoggerFactory>();
            return loggerFactory.CreateLogger(name);
        }
    }
}
