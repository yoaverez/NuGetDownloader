using NuGet.Versioning;
using Playground.NuGetDTO;
using System.Text.Json.Serialization;

namespace Playground.NuGetDTOs
{
    /// <summary>
    /// A DTO for the api's "Catalog entry" object.<br/>
    /// Represents a package's (with a specific version) meta data.
    /// </summary>
    /// <param name="ThisPackageMetaDataUrl">The url from which you can get this package meta data.</param>
    /// <param name="PackageId">The id of the package (usually the package name).</param>
    /// <param name="PackageVersion">The version of the package.</param>
    /// <param name="DependencyGroups">
    /// An enumerable of the package dependencies groups.<br/>
    /// Each group represents the dependencies of different target framework e.g. net7 or net4.8.<br/>
    /// This may be null or an empty enumerable if there are no dependencies.
    /// </param>
    public record PackageMetaData([property: JsonPropertyName("@id")] string ThisPackageMetaDataUrl,
                                  [property: JsonPropertyName("id")] string PackageId,
                                  [property: JsonPropertyName("version")] NuGetVersion PackageVersion,
                                  [property: JsonPropertyName("dependencyGroups")] IEnumerable<DependencyGroup>? DependencyGroups)
    {
    }
}
