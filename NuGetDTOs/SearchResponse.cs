using System.Text.Json.Serialization;

namespace NuGetDTOs
{
    /// <summary>
    /// A DTO for the api's response for a "search" request.
    /// </summary>
    /// <param name="ResultsData">Enumerable containing the results of a search.</param>
    public record SearchResponse([property: JsonPropertyName("data")] IEnumerable<SearchResult> ResultsData)
    {
    }
}
