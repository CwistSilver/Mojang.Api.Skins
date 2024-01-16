using Mojang.Api.Skins.Data;
using Mojang.Api.Skins.ImageService.General;

namespace Mojang.Api.Skins.ImageService.Identifier.Skin;
public sealed class SkinTypeIdentifier : ISkinTypeIdentifier
{
    private readonly IImageUtilities _imageUtilities;
    public SkinTypeIdentifier(IImageUtilities imageUtilities)
    {
        _imageUtilities = imageUtilities;
    }

    public SkinType Identify(ReadOnlySpan<byte> skinBytes)
    {
        var classicPixel = _imageUtilities.GetPixel(skinBytes, 55, 20);
        return classicPixel.A == 255 ? SkinType.Classic : SkinType.Slim;
    }
}
