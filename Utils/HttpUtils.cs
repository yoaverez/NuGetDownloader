using Serialization;
using System.Text;

namespace Utils
{
    /// <summary>
    /// Utility functions for http related tasks.
    /// </summary>
    public static class HttpUtils
    {
        /// <summary>
        /// Make an http get request and read the response contents when it arrives.
        /// </summary>
        /// <typeparam name="T">The type of the contents of the response of the request.</typeparam>
        /// <param name="httpClient">The http client to use for the request.</param>
        /// <param name="request">The request.</param>
        /// <returns>A task that returns the result of the http get request.</returns>
        /// <exception cref="HttpRequestException">
        /// Thrown when the http request failed i.e. does not have a success status.
        /// </exception>
        /// <exception cref="Exception">
        /// Thrown when the request has a success status but for some unexpected reason we
        /// got null in the response contents.
        /// </exception>
        public static async Task<T> ReadContentsOfHttpGetRequestAsync<T>(HttpClient httpClient, string request)
        {
            // Perform the http get request.
            var response = await httpClient.GetAsync(request);

            // Ensure the success of the request.
            response.EnsureSuccessStatusCode();

            // Read the contents of the response to an object.
            var contents = await response.Content.ReadAsStringAsync();
            var deserializedContent = Serializer.Deserialize<T>(contents);

            if (deserializedContent is null)
                // Since we ensure the success status, the deserialization should either work
                // or throw exception if T is not compatible with the contents.
                // So if we got here it means that for some weird reason the response contents was just null.
                throw new Exception("Something went wrong with the deserialization of the response contents.");

            return deserializedContent;
        }

        /// <summary>
        /// A <see cref="Uri"/> paths combiner.
        /// </summary>
        /// <param name="firstUri">The first uri to combine.</param>
        /// <param name="uris">The uris to concatenate to the <paramref name="firstUri"/>.</param>
        /// <returns>A Uri that represent the combination of the given uris.</returns>
        public static Uri UriCombine(string firstUri, params string[] uris)
        {
            var combinedUri = new StringBuilder(firstUri.Trim().TrimEnd('/'));
            foreach (var uri in uris)
            {
                var uriAfterTrim = uri.Trim().Trim('/');
                combinedUri.Append($"/{uriAfterTrim}");
            }
            return new Uri(combinedUri.ToString());
        }
    }
}
