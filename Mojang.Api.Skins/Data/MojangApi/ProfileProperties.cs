using System.Text.Json.Serialization;

namespace Mojang.Api.Skins.Data.MojangApi;
/// <summary>
/// Represents detailed properties of a Minecraft player profile.
/// </summary>
public sealed class ProfileProperties : ProfileInformation
{
    /// <summary>
    /// A collection of properties associated with the player profile.
    /// </summary>
    [JsonPropertyName("properties")]
    public ProfileProperty[]? Properties { get; set; }

    /// <summary>
    /// A collection of actions associated with the player profile.
    /// </summary>
    [JsonPropertyName("profileActions")]
    public object[]? ProfileActions { get; set; }
}
