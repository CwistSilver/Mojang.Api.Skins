using Mojang.Api.Skins.Data;
using Mojang.Api.Skins.Data.MojangApi;

namespace Mojang.Api.Skins.Repository.MinecraftProfileInformation;
/// <summary>
/// Defines an interface for repositories that provide Minecraft player profile information.
/// </summary>
public interface IProfileInformationRepository
{
    /// <summary>
    /// Options for client configurations.
    /// </summary>
    ClientOptions Options { get; set; }

    /// <summary>
    /// Asynchronously retrieves the profile information for a given player name.
    /// </summary>
    /// <param name="playerName">The name of the player whose profile information is being requested.</param>
    /// <returns>A task that represents the asynchronous operation and returns the player's profile information.</returns>
    Task<ProfileInformation> Get(string playerName);
}
