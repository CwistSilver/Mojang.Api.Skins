using Mojang.Api.Skins.Data;
using Mojang.Api.Skins.Data.MojangApi;

namespace Mojang.Api.Skins.Repository.MinecraftProfileProperties;

/// <summary>
/// Defines an interface for repositories that provide Minecraft player profile properties.
/// </summary>
public interface IProfilePropertiesRepository
{
    /// <summary>
    /// Options for client configurations.
    /// </summary>
    ClientOptions Options { get; set; }

    /// <summary>
    /// Asynchronously retrieves the properties of a player's profile using their UUID.
    /// </summary>
    /// <param name="playerUUID">The UUID of the player whose profile properties are being requested.</param>
    /// <returns>A task that represents the asynchronous operation and returns the player's profile properties.</returns>
    Task<ProfileProperties> Get(Guid playerUUID);
}
