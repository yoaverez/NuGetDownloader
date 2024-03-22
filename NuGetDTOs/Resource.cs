using System.Text.Json.Serialization;

namespace NuGetDTOs
{
    /// <summary>
    /// A DTO to the api's "Resource" object.<br/>
    /// Store information about a single api service.
    /// </summary>
    /// <param name="BaseUrl">The base url for getting the resources services.</param>
    /// <param name="Type">The identifier of the resource.</param>
    public record Resource([property: JsonPropertyName("@id")] string BaseUrl,
                           [property: JsonPropertyName("@type")] string Type)
    {
    }
}
