using System.Text.Json;
using System.Text.Json.Serialization;

namespace Mojang.Api.Skins.Utilities;
public sealed class JsonUUIDConverter : JsonConverter<Guid>
{
    public override Guid Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var guidString = reader.GetString() ?? throw new JsonException("UUID is null");

        if (Guid.TryParse(guidString, out Guid guid))
            return guid;

        throw new JsonException($"Invalid UUID format: {guidString}");
    }

    public override void Write(Utf8JsonWriter writer, Guid value, JsonSerializerOptions options) => writer.WriteStringValue(value.ToString("N"));
}
