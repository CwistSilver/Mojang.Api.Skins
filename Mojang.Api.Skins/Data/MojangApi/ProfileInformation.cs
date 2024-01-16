using Mojang.Api.Skins.Utilities;
using System.Text.Json.Serialization;

namespace Mojang.Api.Skins.Data.MojangApi;

/// <summary>
/// Represents the basic information of a Minecraft player profile.
/// </summary>
public class ProfileInformation
{
    /// <summary>
    /// The unique identifier of the player.
    /// </summary>
    [JsonPropertyName("id")]
    [JsonConverter(typeof(JsonUUIDConverter))]
    public Guid Id { get; init; }

    /// <summary>
    /// The name of the player.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    public override string ToString() => $"Player-UUID: {Id:N} Name: {Name}";

}
