using System.Text.Json.Serialization;

namespace Playground.NuGetDTOs
{
    /// <summary>
    /// A DTO for the api's "Registration leaf".<br/>
    /// Represents the information of where the specific package version meta data is stored.
    /// </summary>
    /// <param name="RegistrationLeafUrl">The URL to the registration leaf.</param>
    /// <param name="MetaDataUrl">The URL to this specific package version meta data.</param>
    public record RegistrationLeaf([property: JsonPropertyName("@id")] string RegistrationLeafUrl,
                                   [property: JsonPropertyName("catalogEntry")] string MetaDataUrl)
    {
    }
}
