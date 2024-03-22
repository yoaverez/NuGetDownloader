using System.Text.Json.Serialization;

namespace NuGetDTOs
{
    /// <summary>
    /// A DTO for the api's "Search result" object.
    /// </summary>
    /// <param name="Id">The id of the matched package (usually the package name).</param>
    /// <param name="AvailablePackageVersions">All the available versions for the matched package.</param>
    public record SearchResult([property: JsonPropertyName("id")] string Id,
                               [property: JsonPropertyName("versions")] IEnumerable<PackageVersionInfo> AvailablePackageVersions)
    {
    }
}
