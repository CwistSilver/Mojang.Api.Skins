using Mojang.Api.Skins.ImageService.General;

namespace Mojang.Api.Skins.Data;

/// <summary>
/// Represents the texture data for a specific part of a Minecraft cape.
/// </summary>
public sealed class CapePartData : TextureData
{
    /// <summary>
    /// The specific part of the cape represented by this texture data.
    /// </summary>
    public CapePart CapePart { get; set; }

    public CapePartData(IImageUtilities imageUtilities) : base(imageUtilities) { }
}
