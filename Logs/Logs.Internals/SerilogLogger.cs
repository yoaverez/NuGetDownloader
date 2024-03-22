using Microsoft.Extensions.Configuration;
using Serilog;

namespace Logs.Internals
{
    /// <summary>
    /// A logger that uses serilog.
    /// </summary>
    public class SerilogLogger : Logs.Abstracts.ILogger
    {
        /// <summary>
        /// The underline logger.
        /// </summary>
        private readonly Serilog.Core.Logger logger;

        /// <summary>
        /// Constructor for creation instances of the <see cref="SerilogLogger"/> class.
        /// </summary>
        /// <param name="appsettingsPath">The path to the appsettings that configures the logger.</param>
        public SerilogLogger(string appsettingsPath)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile(appsettingsPath)
                                                          .Build();

            logger = new LoggerConfiguration().ReadFrom.Configuration(configuration)
                                                       .CreateLogger();
        }

        /// <inheritdoc/>
        public void LogCustomInfo(string senderName, string functionName, string infromation)
        {
            logger.Information($"{senderName}.{functionName}: {infromation}");
        }

        /// <inheritdoc/>
        public void LogCustomWarning(string senderName, string functionName, string warning)
        {
            logger.Warning($"{senderName}.{functionName}: {warning}");
        }

        /// <inheritdoc/>
        public void LogCustomError(string senderName, string functionName, string exceptionMessage)
        {
            logger.Error($"{senderName}.{functionName}: {exceptionMessage}");
        }

        /// <inheritdoc/>
        public void LogError(string senderName, string functionName, Exception exception)
        {
            logger.Error(exception, $"{senderName}.{functionName}");
        }
    }
}
