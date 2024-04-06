using Microsoft.Extensions.Configuration;
using NuGetDownloader.CommandLineObjects.Options;
using NuGetDownloader.Configuration;

namespace NuGetDownloader.CommandLineObjects.Executers
{
    /// <summary>
    /// An executer for the command line "run" verb.
    /// </summary>
    public static class RunExecuter
    {
        /// <summary>
        /// Download all the wanted packages and their dependencies.
        /// </summary>
        /// <param name="runOptions">The options for command line for the "run" verb.</param>
        public static async Task<int> ExecuteAsync(RunOptions runOptions)
        {
            var configurationRoot = new ConfigurationBuilder().AddJsonFile(runOptions.AppsettingsPath)
                                                              .Build();
            var config = configurationRoot.Get<DownloaderConfig>();

            foreach (var downloadRequest in config.DownloadRequests)
            {
                var outputDirectoryPath = Path.Combine(config.OutputDirectoryPath, $"{downloadRequest.PackageName}.{downloadRequest.PackageVersion}");
                await NuGetPackagesDownloader.DownloadPackageAndItsDependenciesAsync(outputDirectoryPath, downloadRequest.PackageName, downloadRequest.PackageVersion, config.DownloadBuiltInLibraries);
            }

            // Return success status.
            return 0;
        }
    }
}
