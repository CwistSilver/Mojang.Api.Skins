using System.Text.Json.Serialization;

namespace Mojang.Api.Skins.Data.MojangApi;
/// <summary>
/// Represents the textures associated with a Minecraft player profile.
/// </summary>
public sealed class Textures
{
    /// <summary>
    /// The URL of the player's skin texture.
    /// </summary>
    [JsonPropertyName("SKIN")]
    public TextureUrl Skin { get; set; } = new();

    /// <summary>
    /// The URL of the player's cape texture, if available.
    /// </summary>
    [JsonPropertyName("CAPE")]
    public TextureUrl? Cape { get; set; }
}
