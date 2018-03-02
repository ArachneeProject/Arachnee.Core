using System;
using System.Globalization;

namespace Arachnee.InnerCore.LoggerBases
{
    public class ConsoleLogger : ILogger
    {
        private static string GetUtcNow()
        {
            return DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
        }

        public void LogTrace(object message)
        {
            Console.WriteLine($"{GetUtcNow()} TRACE:{message}");
        }

        public void LogDebug(object message)
        {
            Console.WriteLine($"{GetUtcNow()} DEBUG:{message}");
        }

        public void LogInfo(object message)
        {
            Console.WriteLine($"{GetUtcNow()} INFO:{message}");
        }

        public void LogWarning(object message)
        {
            Console.WriteLine($"{GetUtcNow()} WARNING:{message}");
        }

        public void LogWarning(object message, Exception exception)
        {
            Console.WriteLine($"{GetUtcNow()} WARNING:{message} - Details:{exception}");
        }

        public void LogError(object message)
        {
            Console.WriteLine($"{GetUtcNow()} ERROR:{message}");
        }

        public void LogError(object message, Exception exception)
        {
            Console.WriteLine($"{GetUtcNow()} ERROR:{message} - Details:{exception}");
        }

        public void LogFatal(object message)
        {
            Console.WriteLine($"{GetUtcNow()} FATAL:{message}");
        }

        public void LogFatal(object message, Exception exception)
        {
            Console.WriteLine($"{GetUtcNow()} FATAL:{message} - Details:{exception}");
        }
    }
}