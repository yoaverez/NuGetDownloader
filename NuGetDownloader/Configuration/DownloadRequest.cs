namespace NuGetDownloader.Configuration
{
    /// <summary>
    /// Configuration of a single download request.
    /// </summary>
    public class DownloadRequest
    {
        /// <summary>
        /// The name of the package to download.
        /// </summary>
        public string PackageName { get; set; }

        /// <summary>
        /// The version of the <see cref="PackageName"/> to download.
        /// </summary>
        public string PackageVersion { get; set; }
    }
}
