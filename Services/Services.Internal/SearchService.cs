using Playground.NuGetDTOs;
using Services.Abstracts;
using static Utils.HttpUtils;

namespace Services.Internal
{
    /// <inheritdoc cref="ISearchService"/>
    public class SearchService : HttpService, ISearchService
    {
        /// <summary>
        /// Create new instance of the <see cref="SearchService"/> class.
        /// </summary>
        /// <inheritdoc/>
        public SearchService(string serviceBaseUrl) : base(serviceBaseUrl)
        {
            // Noting to do.
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<SearchResult>> SearchPackageAsync(string packageName)
        {
            var searchResponse = await ReadContentsOfHttpGetRequestAsync<SearchResponse>(httpClient, $"?q=packageid:{packageName}");

            return searchResponse.ResultsData;
        }
    }
}
