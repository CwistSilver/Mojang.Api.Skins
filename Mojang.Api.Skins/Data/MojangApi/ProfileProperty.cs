using System.Text.Json.Serialization;

namespace Mojang.Api.Skins.Data.MojangApi;

/// <summary>
/// Represents a single property within a Minecraft player profile.
/// </summary>
public sealed class ProfileProperty
{
    /// <summary>
    /// The name of the property.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// The value of the property.
    /// </summary>
    [JsonPropertyName("value")]
    public string Value { get; init; } = string.Empty;

    public override string ToString() => $"Name: {Name} Value: {Value}";
}
