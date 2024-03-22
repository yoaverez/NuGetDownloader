using NuGet.Versioning;
using System.Text.Json.Serialization;

namespace NuGetDTOs
{
    /// <summary>
    /// A DTO for the api's "Package dependency" object.<br/>
    /// Represents a single dependency of a package.
    /// </summary>
    /// <param name="PackageDependencyId">The id of the dependency package (usually the package name).</param>
    /// <param name="VersionRange">The range of compatible versions of the dependency.</param>
    public record PackageDependency([property: JsonPropertyName("id")] string PackageDependencyId,
                                    [property: JsonPropertyName("range")] VersionRange VersionRange)
    {
    }
}
