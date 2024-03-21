using NuGet.Versioning;

namespace Services.Abstracts
{
    /// <summary>
    /// A service for the retrieval of a package contents i.e. the contents of the .nupkg file.
    /// </summary>
    public interface IPackageContentsService
    {
        /// <summary>
        /// Download the package with the given <paramref name="packageName"/> and <paramref name="packageVersion"/>
        /// to the given <paramref name="outputDirectory"/>.
        /// </summary>
        /// <param name="packageName">The name of the package to download.</param>
        /// <param name="packageVersion">The wanted version of the package.</param>
        /// <param name="outputDirectory">The directory in which to place the package file.</param>
        /// <returns>A task that downloads the wanted package to the given <paramref name="outputDirectory"/>.</returns>
        Task DownloadPackageAsync(string packageName, NuGetVersion packageVersion, string outputDirectory);
    }
}
