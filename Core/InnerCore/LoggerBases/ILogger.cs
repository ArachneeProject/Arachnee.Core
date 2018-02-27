using System;

namespace Arachnee.InnerCore.LoggerBases
{
    public interface ILogger
    {
        void LogTrace(object message);

        void LogDebug(object message);

        void LogInfo(object message);

        void LogWarning(object message);
        void LogWarning(object message, Exception exception);

        void LogError(object message);
        void LogError(object message, Exception exception);

        void LogFatal(object message);
        void LogFatal(object message, Exception exception);
    }
}