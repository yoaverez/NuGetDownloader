using NuGet.Versioning;
using System.Text.Json.Serialization;

namespace Playground.NuGetDTOs
{
    /// <summary>
    /// A DTO representing a specific package version info.
    /// </summary>
    /// <param name="RegistrationLeafUrl">A url that leads to the specific package version <see cref="RegistrationLeaf"/>.</param>
    /// <param name="Version">The version of the package.</param>
    public record PackageVersion([property: JsonPropertyName("@id")] string RegistrationLeafUrl,
                                 [property: JsonPropertyName("version")] NuGetVersion Version)
    {
    }
}
