using Mojang.Api.Skins.Data;

namespace Mojang.Api.Skins;
/// <summary>
/// Defines the interface for a Minecraft skin client.
/// </summary>
public interface ISkinsClient
{
    /// <summary>
    /// Client configuration options.
    /// </summary>
    ClientOptions Options { get; init; }

    /// <summary>
    /// Asynchronously retrieves the skin data for a specified player by name.
    /// </summary>
    /// <param name="playerName">The name of the player whose skin data is to be retrieved.</param>
    /// <returns>A task that represents the asynchronous operation and contains the skin data of the player.</returns>
    Task<PlayerData> GetAsync(string playerName);

    /// <summary>
    /// Asynchronously retrieves the skin data for a specified player by UUID.
    /// </summary>
    /// <param name="playerUUID">The UUID of the player whose skin data is to be retrieved.</param>
    /// <returns>A task that represents the asynchronous operation and contains the skin data of the player.</returns>
    Task<PlayerData> GetAsync(Guid playerUUID);

    /// <summary>
    /// Retrieves the skin data from provided skin and cape file bytes.
    /// </summary>
    /// <param name="skinFileBytes">The byte array of the skin file.</param>
    /// <param name="capeFileBytes">The byte array of the cape file. This parameter is optional.</param>
    /// <returns>The skin data obtained from the file bytes.</returns>
    PlayerData GetLocal(byte[] skinFileBytes, byte[]? capeFileBytes = null);
}