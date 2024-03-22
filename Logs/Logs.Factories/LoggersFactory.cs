using Logs.Abstracts;
using Logs.Internals;

namespace Logs.Factories
{
    /// <summary>
    /// A class for creating loggers.
    /// </summary>
    public static class LoggersFactory
    {
        /// <summary>
        /// Create new logger.
        /// </summary>
        /// <param name="appsettingsPath">The path to the logger configuration file.</param>
        public static ILogger CreateLogger(string appsettingsPath)
        {
            return new SerilogLogger(appsettingsPath);
        }
    }
}
