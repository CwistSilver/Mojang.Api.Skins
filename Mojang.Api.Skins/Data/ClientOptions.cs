using Mojang.Api.Skins.Cache;
using Mojang.Api.Skins.ImageService.General;

namespace Mojang.Api.Skins.Data;
/// <summary>
/// Represents the client configuration options for Minecraft skin processing.
/// </summary>
public sealed class ClientOptions
{
    /// <summary>
    /// The default <see cref="ClientOptions"/> instance with pre-configured settings.
    /// </summary>
    public static ClientOptions Default => new() { Cache = new LiteDBCache(new SkiaImageUtilities()), ConvertLegacySkin = true };

    /// <summary>
    /// The cache implementation. If set to null, caching is disabled.
    /// </summary>
    /// <remarks>
    /// Implement the <see cref="ICache"/> interface to define custom caching behavior.
    /// </remarks>
    public ICache? Cache { get; set; }

    /// <summary>
    /// A value indicating whether to convert legacy Minecraft skins to the modern format.
    /// </summary>
    /// <value>
    /// true to convert legacy skins; otherwise, false.
    /// </value>
    public bool ConvertLegacySkin { get; set; }
}
