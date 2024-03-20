using Playground.NuGetDTOs;
using System.Text.Json.Serialization;

namespace Playground.NuGetDTO
{
    /// <summary>
    /// A DTO for for the api's "Package dependency group" object.<br/>
    /// Represents a package <b>direct</b> dependencies for use in a specific<paramref name="TargetFramework"/>.
    /// </summary>
    /// <param name="TargetFramework">The target framework of those <paramref name="PackageDependencies"/>.</param>
    /// <param name="PackageDependencies">All the <b>direct</b> dependencies of a package for the <paramref name="TargetFramework"/>.</param>
    /// <remarks>
    /// The <paramref name="PackageDependencies"/> are <b>direct</b> dependencies meaning that
    /// if a package A depends on package B and package B depends on package C
    /// then the <paramref name="PackageDependencies"/> of A will contain <b>only package B</b>.
    /// </remarks>
    public record DependencyGroup([property: JsonPropertyName("targetFramework")] string? TargetFramework,
                                  [property: JsonPropertyName("dependencies")] IEnumerable<PackageDependency> PackageDependencies)
    {
    }
}
