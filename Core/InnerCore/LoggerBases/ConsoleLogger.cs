using System;
using System.Globalization;

namespace Arachnee.InnerCore.LoggerBases
{
    public class ConsoleLogger : ILogger
    {
        public string Name { get; }

        public ConsoleLogger()
        {
            Name = "main";
        }

        private ConsoleLogger(string name)
        {
            Name = name;
        }

        public ILogger CreateSubLoggerFor(string subLoggerName)
        {
            return new ConsoleLogger($"{Name}/{subLoggerName}");
        }

        public void LogDebug(object message)
        {
            Console.WriteLine($"{GetUtcNow()} [{Name}] DEBUG: {message}");
        }

        public void LogError(object message)
        {
            Console.WriteLine($"{GetUtcNow()} [{Name}] ERROR: {message}");
        }

        public void LogError(object message, Exception exception)
        {
            Console.WriteLine($"{GetUtcNow()} [{Name}] ERROR: {message} - Details:{exception}");
        }

        public void LogFatal(object message)
        {
            Console.WriteLine($"{GetUtcNow()} [{Name}] FATAL: {message}");
        }

        public void LogFatal(object message, Exception exception)
        {
            Console.WriteLine($"{GetUtcNow()} [{Name}] FATAL: {message} - Details:{exception}");
        }

        public void LogInfo(object message)
        {
            Console.WriteLine($"{GetUtcNow()} [{Name}] INFO: {message}");
        }

        public void LogTrace(object message)
        {
            Console.WriteLine($"{GetUtcNow()} [{Name}] TRACE: {message}");
        }

        public void LogWarning(object message)
        {
            Console.WriteLine($"{GetUtcNow()} [{Name}] WARNING: {message}");
        }

        public void LogWarning(object message, Exception exception)
        {
            Console.WriteLine($"{GetUtcNow()} [{Name}] WARNING: {message} - Details:{exception}");
        }
        
        private static string GetUtcNow()
        {
            return DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
        }
    }
}