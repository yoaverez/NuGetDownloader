namespace Logs.Abstracts
{
    /// <summary>
    /// A contract for loggers.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Log custom line in the logger with information level.
        /// </summary>
        /// <param name="senderName">The name of the class that log this message.</param>
        /// <param name="functionName">The name of the function that log this message.</param>
        /// <param name="infromation">The data to log.</param>
        void LogCustomInfo(string senderName, string functionName, string infromation);

        /// <summary>
        /// Log custom warning in the logger with warning level.
        /// </summary>
        /// <param name="senderName">The name of the class that log this message.</param>
        /// <param name="functionName">The name of the function that log this message.</param>
        /// <param name="warning">The data to log.</param>
        void LogCustomWarning(string senderName, string functionName, string warning);

        /// <summary>
        /// Log custom error in the logger with Error level.
        /// </summary>
        /// <param name="senderName">The name of the class that log this message.</param>
        /// <param name="functionName">The name of the function that log this message.</param>
        /// <param name="exceptionMessage">The data to log.</param>
        void LogCustomError(string senderName, string functionName, string exceptionMessage);

        /// <summary>
        /// Log an error in the logger with Error level.
        /// </summary>
        /// <param name="senderName">The name of the class that log this message.</param>
        /// <param name="functionName">The name of the function that log this message.</param>
        /// <param name="exception">The data to log.</param>
        void LogError(string senderName, string functionName, Exception exception);
    }
}
