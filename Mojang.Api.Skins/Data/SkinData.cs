using Mojang.Api.Skins.ImageService.General;
using Mojang.Api.Skins.Utilities.TextureCropper;

namespace Mojang.Api.Skins.Data;
/// <summary>
/// Represents the texture data for a Minecraft skin.
/// </summary>
public sealed class SkinData : TextureData
{
    /// <summary>
    /// Gets the type of the skin.
    /// </summary>
    public SkinType SkinType { get; set; }
    public string? TextureUrl { get; set; }

    private readonly ITextureCropper? _skinPartCropper;

    /// <summary>
    /// Initializes a new instance of the <see cref="SkinData"/> class with a specific skin part cropper.
    /// </summary>
    /// <param name="skinPartCropper">The cropper used for skin parts.</param>
    public SkinData(ITextureCropper skinPartCropper, IImageUtilities imageUtilities) : base(imageUtilities) => _skinPartCropper = skinPartCropper;

    /// <summary>
    /// Initializes a new instance of the <see cref="SkinData"/> class.
    /// </summary>
    public SkinData() : base() { }

    /// Retrieves the specified skin part data.
    /// </summary>
    /// <param name="skinPart">The skin part to retrieve data for.</param>
    /// <returns>The data for the specified skin part, or null if the cropper is not set.</returns>
    public SkinPartData? GetSkinPart(SkinPart skinPart) => _skinPartCropper?.Cut(this, skinPart);

    /// <summary>
    /// Retrieves the data for a collection of specified skin parts.
    /// </summary>
    /// <param name="skinParts">The collection of skin parts to retrieve data for.</param>
    /// <returns>An array of skin part data, or null if the cropper is not set.</returns>
    public SkinPartData[]? GetSkinPart(IEnumerable<SkinPart> skinParts) => _skinPartCropper?.Cut(this, skinParts);
}
