using Mojang.Api.Skins.Utilities;
using System.Text.Json.Serialization;

namespace Mojang.Api.Skins.Data.MojangApi;
/// <summary>
/// Represents the texture information associated with a Minecraft player profile.
/// </summary>
public sealed class ProfileTextureInformation
{
    /// <summary>
    /// The timestamp of the texture information, represented as a Unix time in milliseconds.
    /// </summary>
    [JsonPropertyName("timestamp")]
    public long Timestamp { get; init; }

    /// <summary>
    /// The timestamp as a <see cref="DateTime"/> object.
    /// </summary>
    [JsonIgnore]
    public DateTime TimestampAsUnixDateTime => DateTimeOffset.FromUnixTimeMilliseconds(Timestamp).UtcDateTime;

    /// <summary>
    /// The unique identifier of the player profile.
    /// </summary>
    [JsonPropertyName("profileId")]
    [JsonConverter(typeof(JsonUUIDConverter))]
    public Guid ProfileId { get; init; }

    /// <summary>
    /// The name of the player profile.
    /// </summary>
    [JsonPropertyName("profileName")]
    public string ProfileName { get; init; } = string.Empty;

    /// <summary>
    /// The textures associated with the player profile.
    /// </summary>
    [JsonPropertyName("textures")]
    public Textures? Textures { get; init; }
}
