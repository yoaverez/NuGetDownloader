using CommandLine;

namespace NuGetDownloader.CommandLineObjects.Options
{
    /// <summary>
    /// A command line verb for generating an example of an the application configuration
    /// i.e. it's appsetting file.
    /// </summary>
    [Verb("generate-config", HelpText = "Generate an example of how the application configuration file (i.e. appsettings) should look like.")]
    public class GenerateConfigOptions
    {
    }
}
