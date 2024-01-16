using Mojang.Api.Skins.ImageService.General;
using Mojang.Api.Skins.Utilities.TextureCropper;

namespace Mojang.Api.Skins.Data;

/// <summary>
/// Represents the texture data for a Minecraft cape.
/// </summary>
public sealed class CapeData : TextureData
{
    public string CapeName { get; set; } = string.Empty;
    public string? TextureUrl { get; set; }

    private readonly ITextureCropper? _skinPartCropper;

    /// <summary>
    /// Initializes a new instance of the <see cref="CapeData"/> class with a specific skin part cropper.
    /// </summary>
    /// <param name="skinPartCropper">The cropper used for processing parts of the cape texture.</param>
    public CapeData(ITextureCropper skinPartCropper, IImageUtilities imageUtilities) : base(imageUtilities) => _skinPartCropper = skinPartCropper;

    /// <summary>
    /// Initializes a new instance of the <see cref="CapeData"/> class.
    /// </summary>
    public CapeData() : base() { }



    /// <summary>
    /// Retrieves the texture data for a specific part of the cape.
    /// </summary>
    /// <param name="capePart">The cape part for which texture data is to be retrieved.</param>
    /// <returns>The texture data for the specified cape part, or null if the cropper is not set.</returns>
    public CapePartData? GetCapePart(CapePart capePart) => _skinPartCropper?.Cut(this, capePart);

    /// <summary>
    /// Retrieves the texture data for multiple specified parts of the cape.
    /// </summary>
    /// <param name="capeParts">The collection of cape parts for which texture data is to be retrieved.</param>
    /// <returns>An array of texture data corresponding to the specified cape parts, or null if the cropper is not set.</returns>
    public CapePartData[]? GetCapePart(IEnumerable<CapePart> capeParts) => _skinPartCropper?.Cut(this, capeParts);
}
