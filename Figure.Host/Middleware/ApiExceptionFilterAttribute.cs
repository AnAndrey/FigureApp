using Figure.Contracts.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Figure.Host.Middleware
{
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private ILogger<ApiExceptionFilterAttribute> Logger { get; }
        public ApiExceptionFilterAttribute(ILogger<ApiExceptionFilterAttribute> logger) 
        {
            Logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            var e = context.Exception;
            context.Result = new BadRequestObjectResult(new { errors = e.Message });
            switch (e)
            {
                case FigureException fe:
                    Logger.LogError(fe.Message);
                    break;
                default:
                    Logger.LogError(e, e.Message);
                    break;
            }
        }
    }
}
