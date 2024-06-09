using Mojang.Api.Skins.Data.MojangApi;
using System.Text.Json.Serialization;

namespace Mojang.Api.Skins;
[JsonSourceGenerationOptions(WriteIndented = true)]
[JsonSerializable(typeof(ApiErrorResponse))]
[JsonSerializable(typeof(ProfileInformation))]
[JsonSerializable(typeof(ProfileProperties))]
[JsonSerializable(typeof(ProfileTextureInformation))]
internal partial class JsonContext : JsonSerializerContext
{
}
