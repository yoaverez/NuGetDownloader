using System.Text.Json.Serialization;

namespace NuGetDTOs
{
    /// <summary>
    /// A DTO for the api's "Service index" object.<br/>
    /// Contain information on all the available services of the api.
    /// </summary>
    /// <param name="Version">The version of the NuGet's HTTP protocol.</param>
    /// <param name="Resources">An enumerable containing information on all the available services of the NuGet api.</param>
    public record NuGetServiceIndex([property: JsonPropertyName("version")] string Version,
                                    [property: JsonPropertyName("resources")] IEnumerable<Resource> Resources)
    {
    }
}
