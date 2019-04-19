// -----------------------------------------------------------------------
//  <copyright file="DtoJsonPropertyConverter.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.IO.Json
{
    using System;
    using Newtonsoft.Json;

    public class DtoJsonPropertyConverter<TInnerProperty> : JsonConverter<DtoProperty<TInnerProperty>>
    {
        /// <inheritdoc />
        public override void WriteJson(JsonWriter writer, DtoProperty<TInnerProperty> value, JsonSerializer serializer)
        {
            if (!value.IsDefined)
            {
                writer.WriteUndefined();
                return;
            }

            var inner = value.Value;

            serializer.Serialize(writer, inner);
        }

        /// <inheritdoc />
        public override DtoProperty<TInnerProperty> ReadJson(JsonReader reader, Type objectType, DtoProperty<TInnerProperty> existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (!hasExistingValue)
                return new DtoProperty<TInnerProperty>();

            var value = serializer.Deserialize<TInnerProperty>(reader);

            return new DtoProperty<TInnerProperty>(value);
        }
    }
}