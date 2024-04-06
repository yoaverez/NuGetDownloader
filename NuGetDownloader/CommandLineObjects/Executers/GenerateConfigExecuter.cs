using NuGetDownloader.CommandLineObjects.Options;
using NuGetDownloader.Configuration;
using Serialization;

namespace NuGetDownloader.CommandLineObjects.Executers
{
    /// <summary>
    /// Executer for the "generate-config" command line verb.
    /// </summary>
    public static class GenerateConfigExecuter
    {
        /// <summary>
        /// Create and example <see cref="DownloaderConfig"/> and serialize it to file.
        /// </summary>
        /// <param name="generateConfigOptions">The generate-config command line verb options.</param>
        public static int Execute(GenerateConfigOptions generateConfigOptions)
        {
            var exampleConfig = GetExampleConfig();
            var configAsJson = Serializer.Serialize(exampleConfig);
            File.WriteAllText(generateConfigOptions.ConfigFilePath, configAsJson);

            // Return success status.
            return 0;
        }

        /// <returns>An example of a <see cref="DownloaderConfig"/> instance.</returns>
        private static DownloaderConfig GetExampleConfig()
        {
            var config = new DownloaderConfig();
            config.DownloadRequests.Add(new DownloadRequest()
            {
                PackageName = "benchmarkdotnet",
                PackageVersion = "0.13.12",
            });
            return config;
        }
    }
}
