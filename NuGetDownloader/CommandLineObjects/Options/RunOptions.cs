using CommandLine;

namespace NuGetDownloader.CommandLineObjects.Options
{
    /// <summary>
    /// A command line options for the "run" verb.
    /// </summary>
    [Verb("run", HelpText = "Download the wanted packages and their dependencies.")]
    public class RunOptions
    {
        /// <summary>
        /// The path to the appsettings file that containing the configuration of the application.
        /// </summary>
        [Option('p', Default = "./appsettings.json", Required = false, HelpText = "The path to the appsettings file that containing the configuration of the application.")]
        public string AppsettingsPath { get; set; }
    }
}
