using System;

namespace Simple.Library.Logging
{
    /// <summary>
    /// Interface for logger
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Log function should write message to log
        /// </summary>
        /// <param name="source"></param>
        /// <param name="message"></param>
        void Log(string source, string message);
        /// <summary>
        /// LogError function should write error specific messages to log
        /// </summary>
        /// <param name="source"></param>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        void LogError(string source, string message, Exception ex);
    }
}
