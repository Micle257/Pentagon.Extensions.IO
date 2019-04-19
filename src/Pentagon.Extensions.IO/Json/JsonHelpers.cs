// -----------------------------------------------------------------------
//  <copyright file="JsonHelpers.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.IO.Json
{
    using System;
    using JetBrains.Annotations;
    using Newtonsoft.Json;

    /// <summary> Provides helper methods for JSON injection logic. </summary>
    public static class JsonHelpers
    {
        /// <summary> The default Json serializer's setting for the application. </summary>
        public static JsonSerializerSettings DefaultJsonSettings { get; set; } = new JsonSerializerSettings
                                                                                 {
                                                                                         Formatting = Formatting.None,
                                                                                         NullValueHandling = NullValueHandling.Ignore,
                                                                                         DateParseHandling = DateParseHandling.DateTimeOffset
                                                                                 };

        /// <summary> Serializes object graph to JSON representation as string. </summary>
        /// <param name="value"> The object to graph serialize. </param>
        /// <returns> A string representation the Json object. </returns>
        public static string Serialize([NotNull] object value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            return JsonConvert.SerializeObject(value, DefaultJsonSettings);
        }

        /// <summary> Deserializes JSON string representation to object graph. </summary>
        /// <param name="value"> The object to deserialize. </param>
        /// <returns> A object obtained from Json string. </returns>
        public static T Deserialize<T>([NotNull] string value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            return JsonConvert.DeserializeObject<T>(value, DefaultJsonSettings);
        }

        /// <summary> Tries deserialize JSON string representation to object graph. </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="serializedValue"> The serialized value. </param>
        /// <param name="value"> The output object graph. </param>
        /// <returns> <c> true </c> if successfully deserialized; otherwise <c> false </c>. </returns>
        public static bool TryDeserialize<T>([NotNull] string serializedValue, out T value)
        {
            value = default;

            try
            {
                value = Deserialize<T>(serializedValue);
                return true;
            }
            catch (JsonReaderException)
            {
                return false;
            }
            catch (JsonSerializationException)
            {
                return false;
            }
        }

        public static T ReadJsonFile<T>(string path) => Deserialize<T>(path);
    }
}