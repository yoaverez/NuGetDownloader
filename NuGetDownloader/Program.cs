using CommandLine;
using NuGetDownloader.CommandLineObjects.Executers;
using NuGetDownloader.CommandLineObjects.Options;
using Utils;

namespace NuGetDownloader
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<GenerateConfigOptions, RunOptions>(args)
                          .MapResult((GenerateConfigOptions generateOptions) => GenerateConfigExecuter.Execute(generateOptions),
                                     (RunOptions runOptions) => RunExecuter.ExecuteAsync(runOptions).TaskWaiter(),
                                     err => 1);
        }
    }
}