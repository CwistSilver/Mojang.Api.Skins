using Mojang.Api.Skins.Data;

namespace Mojang.Api.Skins.Repository.Options;
/// <summary>
/// A repository for storing and retrieving client options
/// </summary>
public interface IClientOptionsRepository
{
    /// <summary>
    /// Get the client options.
    /// </summary>
    /// <returns> The client options.</returns>
    ClientOptions GetOptions();

    /// <summary>
    /// Get the client options async.
    /// </summary>
    /// <returns> The client options.</returns>
    Task<ClientOptions> GetOptionsAsync();

    /// <summary>
    /// Set the client options async.
    /// </summary>
    /// <returns> The client options.</returns>
    /// <param name="options">Options.</param>
    Task SetOptionsAsync(ClientOptions options);
}

