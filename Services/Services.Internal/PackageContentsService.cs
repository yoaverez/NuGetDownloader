using NuGet.Versioning;
using Services.Abstracts;
using static Utils.HttpUtils;

namespace Services.Internal
{
    /// <inheritdoc cref="IPackageContentsService"/>.
    public class PackageContentsService : HttpService, IPackageContentsService
    {
        /// <summary>
        /// Create new instance of the <see cref="PackageContentsService"/> class.
        /// </summary>
        /// <inheritdoc/>
        public PackageContentsService(string serviceBaseUrl) : base(serviceBaseUrl)
        {
            // Noting to do.
        }

        /// <inheritdoc/>
        public async Task DownloadPackageAsync(string packageName, NuGetVersion packageVersion, string outputDirectory)
        {
            // Arrange the request variables.
            var packageId = packageName.ToLowerInvariant();
            var version = packageVersion.ToNormalizedString().ToLowerInvariant();
            var fileName = $"{packageId}.{version}.nupkg";

            var requestUri = UriCombine(packageId, version, fileName);

            // Request the file contents.
            var response = await httpClient.GetAsync(requestUri);

            // Ensure the success of the request.
            response.EnsureSuccessStatusCode();

            // Read the contents of the package as bytes.
            var readerStream = await response.Content.ReadAsStreamAsync();
            var bytes = new byte[readerStream.Length];
            readerStream.Read(bytes);

            // Write the bytes to a file.
            File.WriteAllBytes(Path.Combine(outputDirectory, fileName), bytes);
        }
    }
}
