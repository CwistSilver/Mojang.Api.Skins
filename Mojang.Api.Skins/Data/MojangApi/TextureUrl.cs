using System.Text.Json.Serialization;

namespace Mojang.Api.Skins.Data.MojangApi;
/// <summary>
/// Contains the URL of a texture.
/// </summary>
public sealed class TextureUrl
{
    /// <summary>
    /// The URL of the texture. Can be null if the texture is not set.
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; set; }
}
