using Serialization.JsonConverters;
using System.Text.Json;

namespace Serialization
{
    /// <summary>
    /// A class for creating default serialization settings for the project.
    /// </summary>
    public static class DefaultSerializationSettings
    {
        /// <returns>A new <see cref="JsonSerializerOptions"/> with a default settings for this project.</returns>
        public static JsonSerializerOptions GetDefaultSerializationSettings()
        {
            var serializationOptions = new JsonSerializerOptions()
            {
                WriteIndented = true,
            };

            // Add custom converters.
            serializationOptions.Converters.Add(new NuGetVersionConverter());
            serializationOptions.Converters.Add(new VersionRangeConverter());

            return serializationOptions;
        }
    }
}
