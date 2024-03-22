using NuGet.Versioning;
using NuGetDTOs;
using Services.Abstracts;
using static Utils.HttpUtils;

namespace Services.Internal
{
    /// <inheritdoc cref="IPackageMetaDataService"/>
    public class PackageMetaDataService : HttpService, IPackageMetaDataService
    {
        /// <inheritdoc cref="ISearchService"/>
        private readonly ISearchService searchService;

        /// <summary>
        /// Create new instance of the <see cref="PackageMetaDataService"/> class.
        /// </summary>
        /// <param name="searchService">A service for searching packages.</param>
        /// <inheritdoc/>
        public PackageMetaDataService(ISearchService searchService) : base()
        {
            this.searchService = searchService;
        }

        /// <inheritdoc/>
        public async Task<PackageMetaData> GetPackageMetaDataAsync(string packageName, NuGetVersion packageVersion)
        {
            // We are not prepared to deal with to search results for the same packageName.
            // So we use "Single" to throw an exception if there are more than one search result.
            var searchResult = (await searchService.SearchPackageAsync(packageName)).Single();

            // Find the information of the package with the wanted version.
            var packageVersionInfo = searchResult.AvailablePackageVersions.First(packageVersionInfo => packageVersionInfo.Version.Equals(packageVersion));

            // Find the registration leaf for the wanted package version
            // since it contains the url for that package version meta data.
            var registrationLeaf = await ReadContentsOfHttpGetRequestAsync<RegistrationLeaf>(httpClient, packageVersionInfo.RegistrationLeafUrl);

            // Retrieve the meta data.
            return await ReadContentsOfHttpGetRequestAsync<PackageMetaData>(httpClient, registrationLeaf.MetaDataUrl);
        }
    }
}
