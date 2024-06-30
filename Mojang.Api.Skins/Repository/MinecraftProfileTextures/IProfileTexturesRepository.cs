using Mojang.Api.Skins.Data;
using Mojang.Api.Skins.Data.MojangApi;

namespace Mojang.Api.Skins.Repository.MinecraftProfileTextures;
/// <summary>
/// Defines an interface for repositories that provide texture data for Minecraft player profiles.
/// </summary>
public interface IProfileTexturesRepository
{
    /// <summary>
    /// Asynchronously retrieves the cape data associated with a given player's profile.
    /// </summary>
    /// <param name="profileProperties">The properties of the player's profile.</param>
    /// <returns>A task that represents the asynchronous operation and returns the cape data, if available; otherwise, null.</returns>
    Task<CapeData?> GetCape(ProfileProperties profileProperties);

    /// <summary>
    /// Asynchronously retrieves the skin data associated with a given player's profile.
    /// </summary>
    /// <param name="profileProperties">The properties of the player's profile.</param>
    /// <returns>A task that represents the asynchronous operation and returns the skin data.</returns>
    Task<SkinData> GetSkin(ProfileProperties profileProperties);

    /// <summary>
    /// Retrieves the skin data from a byte array.
    /// </summary>
    /// <param name="skinBytes">The byte array containing the skin data.</param>
    /// <returns>The skin data.</returns>
    SkinData GetSkinLocal(in byte[] skinBytes);

    /// <summary>
    /// Retrieves the cape data from a byte array.
    /// </summary>
    /// <param name="capeData">The byte array containing the cape data.</param>
    /// <returns>The cape data.</returns>
    CapeData GetCapeLocal(in byte[] capeData);
}
