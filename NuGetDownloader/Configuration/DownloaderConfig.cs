namespace NuGetDownloader.Configuration
{
    /// <summary>
    /// Represents the configuration of the NuGetPackagesDownloader.
    /// </summary>
    public class DownloaderConfig
    {
        /// <summary>
        /// The path to the directory in which to download all the packages.
        /// </summary>
        public string OutputDirectoryPath { get; set; }

        /// <summary>
        /// A flag that indicates if to download packages like System of NetStandard etc.
        /// </summary>
        public bool DownloadBuiltInLibraries { get; set; }

        /// <summary>
        /// A list of all the packages to download.
        /// </summary>
        public List<DownloadRequest> DownloadRequests { get; set; }

        /// <summary>
        /// Constructor for initializing new instances of this class.
        /// </summary>
        public DownloaderConfig()
        {
            OutputDirectoryPath = "./";
            DownloadBuiltInLibraries = false;
            DownloadRequests = new List<DownloadRequest>();
        }
    }
}
