using Utils.BaseController;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Utils.Filters
{
    public class ResponseOperationAsyncActionFilter : IAsyncActionFilter
    {
        private readonly ILogger<ResponseOperationAsyncActionFilter> _logger;
        private ResponseOperation _operation;

        public ResponseOperationAsyncActionFilter(ILogger<ResponseOperationAsyncActionFilter> logger)
        {
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            _operation = NewOperation(context);

            if (context.HttpContext.Request.Headers.ContainsKey(ResponseOperationGlobal.NameHeaderOperationId))
            {
                if (!Guid.TryParse(context.HttpContext.Request.Headers[ResponseOperationGlobal.NameHeaderOperationId], out Guid guidOutput))
                {
                    context.HttpContext.Request.Headers.Remove(ResponseOperationGlobal.NameHeaderOperationId);
                    context.HttpContext.Request.Headers.Add(ResponseOperationGlobal.NameHeaderOperationId, _operation.OperationId.ToString());
                }
                else
                {
                    _operation.OperationId = guidOutput;
                }
            }
            else
            {
                context.HttpContext.Request.Headers.Add(ResponseOperationGlobal.NameHeaderOperationId, _operation.OperationId.ToString());
            }

            if (context.HttpContext.Request.Headers.ContainsKey(ResponseOperationGlobal.NameHeaderOperationName))
            {
                if (string.IsNullOrWhiteSpace(context.HttpContext.Request.Headers[ResponseOperationGlobal.NameHeaderOperationName]))
                {
                    context.HttpContext.Request.Headers.Remove(ResponseOperationGlobal.NameHeaderOperationName);
                    context.HttpContext.Request.Headers.Add(ResponseOperationGlobal.NameHeaderOperationName, _operation.OperationName);
                }
                else
                {
                    _operation.OperationName = context.HttpContext.Request.Headers[ResponseOperationGlobal.NameHeaderOperationName];
                }
            }
            else
            {
                context.HttpContext.Request.Headers.Add(ResponseOperationGlobal.NameHeaderOperationName, _operation.OperationName);
            }

            LogResponseOperation();

            ActionExecutedContext resultContext = await next();

            resultContext.HttpContext.Response.Headers.Add(ResponseOperationGlobal.NameHeaderOperationId, _operation.OperationId.ToString());
            resultContext.HttpContext.Response.Headers.Add(ResponseOperationGlobal.NameHeaderOperationName, _operation.OperationName);

        }

        private void LogResponseOperation()
        {
            _logger.LogInformation("OperationId {OperationId}, OperationName: {OperationName}", _operation.OperationId, _operation.OperationName);
        }

        private ResponseOperation NewOperation(ActionExecutingContext context)
        {
            return new ResponseOperation(Guid.NewGuid(), GetOperationName(context));
        }

        private string GetOperationName(ActionExecutingContext context)
        {
            try
            {
                ControllerActionDescriptor actionDescriptor = (ControllerActionDescriptor)context.ActionDescriptor;
                return $"{actionDescriptor.ControllerName}/{actionDescriptor.ActionName}";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return GetOperationName(context.HttpContext);
            }
        }

        private string GetOperationName(HttpContext httpContext)
        {
            try
            {
                return httpContext.Request.Path;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return "Controller/Action|Path";
            }
        }
    }
}
