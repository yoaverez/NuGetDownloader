using NuGet.Versioning;
using Playground.NuGetDTOs;

namespace Services.Abstracts
{
    /// <summary>
    /// A service for retrieving packages meta data.
    /// </summary>
    public interface IPackageMetaDataService
    {
        /// <summary>
        /// Retrieves the meta data of the package with the given <paramref name="packageName"/> and <paramref name="packageVersion"/>.
        /// </summary>
        /// <param name="packageName">The name of the package whose meta data you want.</param>
        /// <param name="packageVersion">The version of the package whose meta data you want.</param>
        /// <returns>A task that returns the meta data of the wanted package.</returns>
        Task<PackageMetaData> GetPackageMetaDataAsync(string packageName, NuGetVersion packageVersion);
    }
}
