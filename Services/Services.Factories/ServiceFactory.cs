using NuGetDTOs;
using Services.Abstracts;
using Services.Internal;
using Utils;
using static Utils.HttpUtils;

namespace Services.Factories
{
    /// <inheritdoc cref="IServiceFactory"/>
    public class ServiceFactory : IServiceFactory
    {
        /// <summary>
        /// The nuget api services index.
        /// </summary>
        private readonly NuGetServiceIndex serviceIndex;

        /// <inheritdoc cref="ISearchService"/>
        private ISearchService searchService;

        /// <inheritdoc cref="IPackageContentsService"/>
        private IPackageContentsService packageContentsService;

        /// <inheritdoc cref="IPackageMetaDataService"/>
        private IPackageMetaDataService packageMetaDataService;

        /// <summary>
        /// A constant that contains the urls to the nuget api index.
        /// </summary>
        private const string NUGET_MAIN_URL = "https://api.nuget.org/v3/index.json";

        /// <summary>
        /// The name of the search service resource.
        /// </summary>
        private const string SEARCH_SERVICE_IDENTIFYER = "SearchQueryService";

        /// <summary>
        /// The name of the package contents service resource.
        /// </summary>
        private const string PACKAGE_CONTENTS_SERVICE_IDENTIFYER = "PackageBaseAddress/3.0.0";

        #region Singleton.

        /// <summary>
        /// The only instance of this class.
        /// </summary>
        public static ServiceFactory Instance { get; }

        /// <summary>
        /// A static constructor to initialize the class singleton.
        /// </summary>
        static ServiceFactory()
        {
            Instance = new ServiceFactory();
        }

        /// <summary>
        /// Private constructor for creating the class singleton.
        /// </summary>
        private ServiceFactory()
        {
            var httpClient = new HttpClient();
            serviceIndex = GetServiceIndex(httpClient);
            CreateServices();
        }

        #endregion Singleton.

        /// <inheritdoc/>
        public IPackageContentsService GetPackageContentsService()
        {
            return packageContentsService;
        }

        /// <inheritdoc/>
        public IPackageMetaDataService GetPackageMetaDataService()
        {
            return packageMetaDataService;
        }

        /// <inheritdoc/>
        public ISearchService GetSearchService()
        {
            return searchService;
        }

        /// <summary>
        /// Get the nuget api service index.
        /// </summary>
        /// <param name="httpClient">The http client to use to get the service index.</param>
        /// <returns></returns>
        private static NuGetServiceIndex GetServiceIndex(HttpClient httpClient)
        {
            return ReadContentsOfHttpGetRequestAsync<NuGetServiceIndex>(httpClient, NUGET_MAIN_URL).TaskWaiter();
        }

        /// <summary>
        /// Get the resource of the nuget api with the given <paramref name="resourceName"/>.
        /// </summary>
        /// <param name="serviceIndex">The nuget api resources index.</param>
        /// <param name="resourceName">The name of the wanted resource.</param>
        /// <returns>A new <see cref="Resource"/> representing the resource with the given <paramref name="resourceName"/>.</returns>
        private static Resource GetResource(NuGetServiceIndex serviceIndex, string resourceName)
        {
            // We use first since some of the resource are duplication.
            // e.g. the SEARCH_SERVICE_IDENTIFYER has to resources with this name
            // a primary search service and a secondary search service.
            return serviceIndex.Resources.First(resource => resource.Type.Equals(resourceName));
        }

        /// <summary>
        /// Create instances of all the services.
        /// </summary>
        private void CreateServices()
        {
            var searchResource = GetResource(serviceIndex, SEARCH_SERVICE_IDENTIFYER);
            searchService = new SearchService(searchResource.BaseUrl);

            var packageContentsResource = GetResource(serviceIndex, PACKAGE_CONTENTS_SERVICE_IDENTIFYER);
            packageContentsService = new PackageContentsService(packageContentsResource.BaseUrl);

            packageMetaDataService = new PackageMetaDataService(searchService);
        }
    }
}
