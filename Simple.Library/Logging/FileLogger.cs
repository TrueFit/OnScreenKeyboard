using System;
using System.IO;

namespace Simple.Library.Logging
{
    /// <summary>
    /// File Logging class to log messages and errors to a flat file
    /// </summary>
    public class FileLogger : ILogger
    {
        private string FileLocation { get; set; }

        /// <summary>
        /// File Logger constructer to set file location
        /// </summary>
        /// <param name="_fileLocation"></param>
        public FileLogger(string _fileLocation)
        {
            FileLocation = _fileLocation;
        }
        /// <summary>
        /// Log general message
        /// </summary>
        /// <param name="source"></param>
        /// <param name="message"></param>
        public void Log(string source, string message)
        {
            if(string.IsNullOrEmpty(source))
            {
                source = "N/A";
            }
            if(string.IsNullOrEmpty(message))
            {
                message = "N/A";
            }
            string formattedMessage = string.Format("TYPE: {0}\nWHEN: {1}\nWHERE: {2}\nMESSAGE: {3}\n\n", "LOG", DateTime.UtcNow.ToString(), source, message);
            WriteMessage(formattedMessage);
        } 
        /// <summary>
        /// Log error
        /// </summary>
        /// <param name="source"></param>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public void LogError(string source, string message, Exception ex)
        {
            if (string.IsNullOrEmpty(source))
            {
                source = "N/A";
            }
            if (string.IsNullOrEmpty(message))
            {
                message = "N/A";
            }
            string formattedMessage = string.Format("TYPE: {0}\nWHEN: {1}\nWHERE: {2}\nMESSAGE: {3}\nERROR: {4}\n\n", "ERROR", DateTime.UtcNow.ToString(), source, message, ex.ToString());
            WriteMessage(formattedMessage);
        }
        /// <summary>
        /// Write message/error to file
        /// </summary>
        /// <param name="formattedMessage"></param>
        private void WriteMessage(string formattedMessage)
        {
            using (StreamWriter streamWriter = new StreamWriter(FileLocation))
            {
                streamWriter.WriteLine(formattedMessage);
                streamWriter.Close();
            }
        }
    }
}
