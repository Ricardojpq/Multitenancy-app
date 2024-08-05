using Microsoft.Extensions.Logging;

namespace SharedKernel.CoreModels.Logging
{
    public interface ICoreLogger
    {
        public void LogInformation(string message);
        void LogInformation(EventId eventId, string message, params object[] args);
        void LogError(EventId eventId, Exception exception, string message);
        void LogError(EventId eventId, string message);
    }
}