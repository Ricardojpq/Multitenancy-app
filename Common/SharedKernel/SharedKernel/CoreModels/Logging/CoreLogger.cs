using SharedKernel.Configurations.Interfaces;
using Microsoft.Extensions.Logging;
using Serilog;

namespace SharedKernel.CoreModels.Logging
{
    public class CoreLogger : ICoreLogger
    {
        private readonly string _correlationId;

        public CoreLogger(ICorrelationIdAccessor correlationContext)
        {
            _correlationId = correlationContext.GetCorrelationId();
        }

        public void LogInformation(string message)
        {
            Log.Information("[CoreLogger] {@message} {@operationId}", new { Message = message }, GetOperationId());
        }

        public void LogInformation(EventId eventId, string message, params object[] args)
        {
            Log.Information("[CoreLogger] {@eventId} {@operationId} {@message} {@args}", eventId, GetOperationId(), new { Message = message }, args);
        }

        public void LogError(EventId eventId, Exception exception, string message)
        {
            Log.Error("[CoreLogger] {@eventId} {@operationId} {@stackTrace} {@message}", eventId, GetOperationId(), exception.StackTrace, new { Message = message });
        }

        public void LogError(EventId eventId, string message)
        {
            Log.Error("[CoreLogger] {@eventId} {@operationId} {@message}", eventId, GetOperationId(), new { Message = message });
        }

        private Object GetOperationId()
        {
            return new
            {
                OperationId = _correlationId
            };
        }
    }
}