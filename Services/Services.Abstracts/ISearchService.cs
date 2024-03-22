using NuGetDTOs;

namespace Services.Abstracts
{
    /// <summary>
    /// A service for searching packages.
    /// </summary>
    public interface ISearchService
    {
        /// <summary>
        /// Search for a package with the given <paramref name="packageName"/>.
        /// </summary>
        /// <param name="packageName">The name of the package to search for.</param>
        /// <returns>A task that will return all the search results.</returns>
        Task<IEnumerable<SearchResult>> SearchPackageAsync(string packageName);
    }
}
