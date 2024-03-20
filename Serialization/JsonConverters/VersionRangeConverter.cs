using NuGet.Versioning;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Serialization.JsonConverters
{
    /// <summary>
    /// A json converter for the <see cref="VersionRange"/>.
    /// </summary>
    public class VersionRangeConverter : JsonConverter<VersionRange>
    {
        /// <inheritdoc/>
        public override VersionRange? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return VersionRange.Parse(reader.GetString()!);
        }

        /// <inheritdoc/>
        public override void Write(Utf8JsonWriter writer, VersionRange value, JsonSerializerOptions options)
        {
            // In this project there is only need in deserialization.
            // Therefore the Write method is not implemented.
            throw new NotImplementedException();
        }
    }
}
