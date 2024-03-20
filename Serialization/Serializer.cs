using System.Text.Json;

namespace Serialization
{
    /// <summary>
    /// A class for serialization and deserialization of objects.
    /// </summary>
    public static class Serializer
    {
        /// <summary>
        /// Serialize the given <paramref name="objectToSerialize"/> to json.
        /// </summary>
        /// <typeparam name="T">The type of the object to serialize.</typeparam>
        /// <param name="objectToSerialize">The object to serialize.</param>
        /// <param name="serializationOptions">
        /// The settings of the serialization.
        /// Default is null and it's converted to <see cref="DefaultSerializationSettings.GetDefaultSerializationSettings"/>.
        /// </param>
        /// <returns>A string representing the object's json form.</returns>
        /// <inheritdoc cref="JsonSerializer.Serialize{TValue}(TValue , JsonSerializerOptions)"/>
        public static string Serialize<T>(T objectToSerialize, JsonSerializerOptions? serializationOptions = null)
        {
            serializationOptions ??= DefaultSerializationSettings.GetDefaultSerializationSettings();
            return JsonSerializer.Serialize(objectToSerialize, serializationOptions);
        }

        /// <summary>
        /// Deserialize the given <paramref name="objectAsJson"/> from json to <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of the object to deserialize.</typeparam>
        /// <param name="objectAsJson">The object in a json form.</param>
        /// <param name="serializationOptions">
        /// The settings of the deserialization.
        /// Default is null and it's converted to <see cref="DefaultSerializationSettings.GetDefaultSerializationSettings"/>.
        /// </param>
        /// <returns>A <typeparamref name="T"/> representation of the <paramref name="objectAsJson"/> value.</returns>
        /// <inheritdoc cref="JsonSerializer.Deserialize{TValue}(string , JsonSerializerOptions)"/>
        public static T? Deserialize<T>(string objectAsJson, JsonSerializerOptions? serializationOptions = null)
        {
            serializationOptions ??= DefaultSerializationSettings.GetDefaultSerializationSettings();
            return JsonSerializer.Deserialize<T>(objectAsJson, serializationOptions);
        }
    }
}
