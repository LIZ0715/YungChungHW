using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Biomedica.NGS.Infrastructure.Filters
{
    public class ApiLoggingFilterAttribute : ActionFilterAttribute, IExceptionFilter
    {
        private readonly ILogger<ApiLoggingFilterAttribute> _logger;

        public ApiLoggingFilterAttribute(ILogger<ApiLoggingFilterAttribute> logger)
        {
            _logger = logger;
        }

        // 記錄 Action 執行前的日誌
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var sessionId = context.HttpContext.Request.Headers["sessionId"];
            using (NLog.ScopeContext.PushProperty("SessionId", sessionId))
            {

                var message = BuildLogMessage(context, "Action Executing");
                _logger.LogInformation(message);

                base.OnActionExecuting(context);
            }
        }

        // 記錄 Action 執行後的日誌
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var sessionID = context.HttpContext.Request.Headers["sessionId"];
            using (NLog.ScopeContext.PushProperty("SessionId", sessionID))
            {
                var message = BuildLogMessage(context, "Action Executed");
                _logger.LogInformation(message);

                base.OnActionExecuted(context);
            }
        }

        // 記錄例外日誌
        public void OnException(ExceptionContext context)
        {
            var sessionID = context.HttpContext.Request.Headers["sessionId"];
            using (NLog.ScopeContext.PushProperty("SessionId", sessionID))
            {
                var actionName = context.ActionDescriptor.RouteValues["action"];
                var controllerName = context.ActionDescriptor.RouteValues["controller"];
                var message = $"例外發生於 {controllerName} - {actionName}: {context.Exception.Message}";

                _logger.LogError(context.Exception, message);
            }
        }

        private string BuildLogMessage(FilterContext context, string logType)
        {
            var actionName = context.ActionDescriptor.RouteValues["action"];
            var controllerName = context.ActionDescriptor.RouteValues["controller"];
            string actionArguments = context is ActionExecutingContext executingContext ? JsonConvert.SerializeObject(executingContext.ActionArguments) : null;
            var ipAddress = GetClientIp(context.HttpContext.Request);
            var threadId = Thread.CurrentThread.ManagedThreadId;
            return $"IP Address: {ipAddress} | thread ID: {threadId} | {logType}: {controllerName} - {actionName} | Parameters: {actionArguments}";
        }

        private bool ShouldSanitize(string controllerName, string actionName)
        {
            // 這裡可以擴展至從配置文件中讀取需要脫敏的控制器和操作名稱
            return controllerName.Equals("Login", StringComparison.OrdinalIgnoreCase) &&
                   actionName.Equals("Post", StringComparison.OrdinalIgnoreCase);
        }

        private string GetClientIp(Microsoft.AspNetCore.Http.HttpRequest request)
        {
            return request.HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unkown";
        }
    }
}
