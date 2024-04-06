using CommandLine;

namespace NuGetDownloader.CommandLineObjects.Options
{
    /// <summary>
    /// A command line options for the "run" verb.
    /// </summary>
    [Verb("run", HelpText = "Download the wanted packages and their dependencies.")]
    public class RunOptions
    {
    }
}
