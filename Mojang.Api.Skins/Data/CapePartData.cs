using Mojang.Api.Skins.ImageService.General;

namespace Mojang.Api.Skins.Data;

/// <summary>
/// Represents the texture data for a specific part of a Minecraft cape.
/// </summary>
public sealed class CapePartData(IImageUtilities imageUtilities) : TextureData(imageUtilities)
{
    /// <summary>
    /// The specific part of the cape represented by this texture data.
    /// </summary>
    public CapePart CapePart { get; set; }
}
