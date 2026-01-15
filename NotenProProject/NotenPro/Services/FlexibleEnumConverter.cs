using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HTLKrems.GradeManagement.Services.Json;

public sealed class FlexibleEnumConverter<TEnum> : JsonConverter<TEnum>
    where TEnum : struct, Enum
{
    public override TEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        // number token: 0,1,2...
        if (reader.TokenType == JsonTokenType.Number)
        {
            var intValue = reader.GetInt32();
            return (TEnum)Enum.ToObject(typeof(TEnum), intValue);
        }

        // string token: "Approved" or "0"
        if (reader.TokenType == JsonTokenType.String)
        {
            var str = reader.GetString();

            if (string.IsNullOrWhiteSpace(str))
                return default;

            if (int.TryParse(str, out var intValue))
                return (TEnum)Enum.ToObject(typeof(TEnum), intValue);

            if (Enum.TryParse<TEnum>(str, ignoreCase: true, out var parsed))
                return parsed;
        }

        throw new JsonException($"Cannot convert token '{reader.TokenType}' to {typeof(TEnum).Name}.");
    }

    public override void Write(Utf8JsonWriter writer, TEnum value, JsonSerializerOptions options)
    {
        // Schreibe als Zahl (passt zu DB-Enums / int-Enums)
        writer.WriteNumberValue(Convert.ToInt32(value));
    }
}