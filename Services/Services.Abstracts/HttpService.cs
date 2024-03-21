namespace Services.Abstracts
{
    /// <summary>
    /// An abstract class representing a service that communicates in http.
    /// </summary>
    public abstract class HttpService
    {
        /// <summary>
        /// The http client to request data.
        /// </summary>
        protected readonly HttpClient httpClient;

        /// <summary>
        /// Constructor for initializing the class fields and properties.
        /// </summary>
        /// <param name="serviceBaseUrl">The base url to use in requests for this service.</param>
        public HttpService(string? serviceBaseUrl = null)
        {
            httpClient = new HttpClient();

            if (serviceBaseUrl is not null)
                httpClient.BaseAddress = new Uri(serviceBaseUrl);
        }
    }
}
