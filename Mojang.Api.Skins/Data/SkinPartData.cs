using Mojang.Api.Skins.ImageService.General;

namespace Mojang.Api.Skins.Data;
/// <summary>
/// Represents the texture data for a specific part of a Minecraft skin.
/// </summary>
public sealed class SkinPartData : TextureData
{
    /// <summary>
    /// The specific part of the skin represented by this texture data.
    /// </summary>
    public SkinPart SkinPart { get; set; }

    public SkinPartData(IImageUtilities imageUtilities) : base(imageUtilities) { }
}
