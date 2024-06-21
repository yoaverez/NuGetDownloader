using Logs.Abstracts;
using Logs.Factories;
using Microsoft.Extensions.DependencyModel;
using NuGet.Versioning;
using NuGetDTOs;
using Services.Abstracts;
using Services.Factories;
using System.Reflection;

namespace NuGetDownloader
{
    /// <summary>
    /// A downloader for nuget packages.
    /// </summary>
    public static class NuGetPackagesDownloader
    {
        /// <inheritdoc cref="IPackageMetaDataService"/>
        private static readonly IPackageMetaDataService packageMetaDataService;

        /// <inheritdoc cref="IPackageContentsService"/>
        private static readonly IPackageContentsService packageContentsService;

        /// <inheritdoc cref="ISearchService"/>
        private static readonly ISearchService searchService;

        /// <summary>
        /// Logger to log data on the <see cref="NuGetPackagesDownloader"/> run.
        /// </summary>
        private static readonly ILogger logger;

        /// <summary>
        /// The name of the appsettings file for the logger.
        /// </summary>
        private const string LOGGER_APPSETTINGS_NAME = "appsettings.logger.json";

        /// <summary>
        /// Constructor to initialize the class static fields and properties.
        /// </summary>
        static NuGetPackagesDownloader()
        {
            var executableDirectory = AppContext.BaseDirectory;
            logger = LoggersFactory.CreateLogger(Path.Combine(executableDirectory, LOGGER_APPSETTINGS_NAME));
            packageMetaDataService = ServiceFactory.Instance.GetPackageMetaDataService();
            packageContentsService = ServiceFactory.Instance.GetPackageContentsService();
            searchService = ServiceFactory.Instance.GetSearchService();
        }

        /// <summary>
        /// Download the .nupkg of the package with the given <paramref name="packageName"/> and <paramref name="wantedVersion"/>
        /// and all of it's dependencies to the given <paramref name="outputDirectoryPath"/>.
        /// </summary>
        /// <param name="outputDirectoryPath">The directory to download the package and it's dependencies to.</param>
        /// <param name="packageName">The name of the package to download.</param>
        /// <param name="wantedVersion">The version of the package to download.</param>
        /// <param name="downloadBuiltInLibraries">Whether or not to download built-in dependencies like "System".</param>
        /// <returns>A running task that downloads the package and all it's dependencies.</returns>
        /// <exception cref="ArgumentException">Thrown when the given <paramref name="wantedVersion"/> is illegal version.</exception>
        public static async Task DownloadPackageAndItsDependenciesAsync(string outputDirectoryPath, string packageName, string wantedVersion, bool downloadBuiltInLibraries = false)
        {
            if (NuGetVersion.TryParse(wantedVersion, out var version))
            {
                // If the directory does not exists then create it.
                if (!Directory.Exists(outputDirectoryPath))
                    Directory.CreateDirectory(outputDirectoryPath);

                await DownloadPackageAndItsDependenciesAsync(outputDirectoryPath, packageName, version, new HashSet<string>(), downloadBuiltInLibraries);
            }
            else
            {
                throw new ArgumentException($"The given {nameof(wantedVersion)}: {wantedVersion} is not a legal version.");
            }
        }

        /// <inheritdoc cref="DownloadPackageAndItsDependenciesAsync(string, string, string, bool)"/>
        /// <param name="alreadyPassThrough">A set that contains the identifiers of packages that we already pass through.</param>
        private static async Task DownloadPackageAndItsDependenciesAsync(string outputDirectoryPath, string packageName, NuGetVersion wantedVersion, HashSet<string> alreadyPassThrough, bool downloadBuiltInLibraries)
        {
            var packageAndVersionIdentifier = GetPackageAndVersionIdentifier(packageName, wantedVersion);
            if (!alreadyPassThrough.Contains(packageAndVersionIdentifier))
            {
                alreadyPassThrough.Add(packageAndVersionIdentifier);

                try
                {
                    // Download the package.
                    logger.LogCustomInfo(nameof(NuGetPackagesDownloader), nameof(DownloadPackageAndItsDependenciesAsync), $"Downloading {packageName}.{wantedVersion.ToNormalizedString().ToLowerInvariant()}");
                    await packageContentsService.DownloadPackageAsync(packageName, wantedVersion, outputDirectoryPath);
                }
                catch (Exception ex)
                {
                    logger.LogError(nameof(NuGetPackagesDownloader), nameof(DownloadPackageAndItsDependenciesAsync), ex);
                    logger.LogCustomError(nameof(NuGetPackagesDownloader),
                                          nameof(DownloadPackageAndItsDependenciesAsync),
                                          $"the {nameof(packageName)}: {packageName} or {nameof(wantedVersion)}: {wantedVersion} are incorrect.");
                    return;
                }

                // Get package meta data.
                var packageMetaData = await packageMetaDataService.GetPackageMetaDataAsync(packageName, wantedVersion);

                // Get all the direct dependencies of the package.
                var packageDependencies = packageMetaData.DependencyGroups?.SelectMany(dependencyGroup => dependencyGroup.PackageDependencies ?? Enumerable.Empty<PackageDependency>()) ?? Enumerable.Empty<PackageDependency>();

                if (!downloadBuiltInLibraries)
                    packageDependencies = packageDependencies.IgnoreBuiltinDependencies();

                // Download all the package dependencies.
                foreach (var dependency in packageDependencies)
                {
                    var dependencyName = dependency.PackageDependencyId;

                    // Search the version that fits the dependency version range best.
                    var dependencyVersion = await GetDependencyVersionToDownloadAsync(dependency);

                    // There was version that fits the version range.
                    if (dependencyVersion is not null)
                    {
                        await DownloadPackageAndItsDependenciesAsync(outputDirectoryPath, dependencyName, dependencyVersion, alreadyPassThrough, downloadBuiltInLibraries);
                    }
                }
            }
        }

        /// <summary>
        /// Get the best version (latests) that fits the given <paramref name="dependency"/> version range.
        /// </summary>
        /// <param name="dependency">The dependency whose best version we want.</param>
        /// <returns>A task that results in the best version (latests) that fits the given <paramref name="dependency"/> version range.</returns>
        private static async Task<NuGetVersion?> GetDependencyVersionToDownloadAsync(PackageDependency dependency)
        {
            var dependencyName = dependency.PackageDependencyId;

            var dependencySearchResults = await searchService.SearchPackageAsync(dependencyName);

            // There are no search result.
            if (!dependencySearchResults.Any())
            {
                logger.LogCustomWarning(nameof(NuGetPackagesDownloader), nameof(DownloadPackageAndItsDependenciesAsync), $"Could not find the dependency {dependencyName}");
                logger.LogCustomWarning(nameof(NuGetPackagesDownloader), nameof(DownloadPackageAndItsDependenciesAsync), $"Skipping the download of the dependency {dependencyName}");
                return null;
            }

            if (dependencySearchResults.Count() > 1)
            {
                logger.LogCustomWarning(nameof(NuGetPackagesDownloader), nameof(DownloadPackageAndItsDependenciesAsync), $"There are more then one package that matched the dependency name {dependencyName}. Taking the first.");
            }

            var dependencySearchResult = dependencySearchResults.First();

            var dependencyVersion = dependency.VersionRange.FindBestMatch(dependencySearchResult.AvailablePackageVersions.Select(packageRangeInfo => packageRangeInfo.Version));

            // There was no version that fits the version range.
            if (dependencyVersion is null)
            {
                logger.LogCustomWarning(nameof(NuGetPackagesDownloader), nameof(DownloadPackageAndItsDependenciesAsync), $"Could not find a version of the {dependencyName} that satisfy the {dependency.VersionRange}");
                logger.LogCustomWarning(nameof(NuGetPackagesDownloader), nameof(DownloadPackageAndItsDependenciesAsync), $"Skipping the download of the dependency {dependencyName}");
            }

            return dependencyVersion;
        }

        /// <summary>
        /// Create an identifier for specific package with a specific version.
        /// </summary>
        /// <param name="packageName">The name of the package.</param>
        /// <param name="version">The version of the package.</param>
        /// <returns>A unique string for this package version.</returns>
        private static string GetPackageAndVersionIdentifier(string packageName, NuGetVersion version)
        {
            return $"{packageName.ToLowerInvariant()}.{version.ToNormalizedString().ToLowerInvariant()}";
        }

        /// <summary>
        /// Ignore all built-in dependencies like "System".
        /// </summary>
        /// <param name="packageDependencies">The enumerable to filter from.</param>
        /// <returns>The enumerable after removing all the built-in dependencies.</returns>
        private static IEnumerable<PackageDependency> IgnoreBuiltinDependencies(this IEnumerable<PackageDependency> packageDependencies)
        {
            return packageDependencies.Where(packageDependency => !packageDependency.PackageDependencyId.ToLowerInvariant().StartsWith("System".ToLowerInvariant())
                                                                  && !packageDependency.PackageDependencyId.ToLowerInvariant().StartsWith("NETStandard".ToLowerInvariant()));
        }
    }
}
