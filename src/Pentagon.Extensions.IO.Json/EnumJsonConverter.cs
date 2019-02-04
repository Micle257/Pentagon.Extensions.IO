// -----------------------------------------------------------------------
//  <copyright file="EnumJsonConverter.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.IO.Json
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using Helpers;
    using Newtonsoft.Json;

    public class EnumJsonConverter<T> : JsonConverter
            where T : Enum
    {
        /// <inheritdoc />
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(((Enum) value).GetDescription());
        }

        /// <inheritdoc />
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null)
                return null;

            var enums = reader.Value as string;

            if (string.IsNullOrEmpty(enums))
                return 0;

            var result = EnumHelper.GetValues<T, DescriptionAttribute>().FirstOrDefault(e => e.Item2?.Description == enums).Item1;

            return result as Enum;
        }

        /// <inheritdoc />
        public override bool CanConvert(Type objectType) => objectType == typeof(string);
    }
}