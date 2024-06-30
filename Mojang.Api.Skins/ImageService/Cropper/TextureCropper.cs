using Mojang.Api.Skins.Data;
using Mojang.Api.Skins.ImageService.General;
using System.Drawing;

namespace Mojang.Api.Skins.Utilities.TextureCropper;
public sealed class TextureCropper(IImageUtilities imageUtilities) : ITextureCropper
{
    private readonly IImageUtilities _imageUtilities = imageUtilities;

    public SkinPartData[] Cut(SkinData skinData, IEnumerable<SkinPart> skinParts)
    {
        var skinPartDatas = new SkinPartData[skinParts.Count()];
        var regions = new Rectangle[skinParts.Count()];
        int index = 0;
        foreach (var skinPart in skinParts)
        {
            Rectangle boundaries;
            if (skinData.SkinType == SkinType.Classic)
                boundaries = TextureComponentsDictionary.ClassicMappings[skinPart];
            else
                boundaries = TextureComponentsDictionary.SlimMappings[skinPart];

            regions[index] = boundaries;
            index++;
        }

        var imageBytes = _imageUtilities.CropImage((ReadOnlySpan<byte>)skinData.TextureBytes.AsSpan(), regions);
        index = 0;
        foreach (var skinPart in skinParts)
        {
            var cropedSkinData = new SkinPartData(_imageUtilities)
            {
                SkinPart = skinPart,
                TextureBytes = imageBytes[index],
                TextureSize = regions[index].Size,
            };

            skinPartDatas[index] = cropedSkinData;
            index++;
        }

        return skinPartDatas;
    }

    public SkinPartData Cut(SkinData skinData, SkinPart skinPart)
    {
        if (skinData.SkinType == SkinType.Classic)
        {
            var steveBoundaries = TextureComponentsDictionary.ClassicMappings[skinPart];
            return new SkinPartData(_imageUtilities)
            {
                SkinPart = skinPart,
                TextureBytes = _imageUtilities.CropImage((ReadOnlySpan<byte>)skinData.TextureBytes.AsSpan(), steveBoundaries),
                TextureSize = steveBoundaries.Size,
            };
        }

        var alexBoundaries = TextureComponentsDictionary.SlimMappings[skinPart];
        return new SkinPartData(_imageUtilities)
        {
            SkinPart = skinPart,
            TextureBytes = _imageUtilities.CropImage((ReadOnlySpan<byte>)skinData.TextureBytes.AsSpan(), alexBoundaries)
        };
    }

    public CapePartData[] Cut(CapeData capeData, IEnumerable<CapePart> capeParts)
    {
        var capePartDatas = new CapePartData[capeParts.Count()];
        var regions = new Rectangle[capeParts.Count()];
        int index = 0;
        foreach (var capePart in capeParts)
        {
            var boundaries = TextureComponentsDictionary.CapeMappings[capePart];

            regions[index] = boundaries;
            index++;
        }

        var imageBytes = _imageUtilities.CropImage((ReadOnlySpan<byte>)capeData.TextureBytes.AsSpan(), regions);
        index = 0;
        foreach (var capePart in capeParts)
        {
            var cropedCapeData = new CapePartData(_imageUtilities)
            {
                CapePart = capePart,
                TextureBytes = imageBytes[index],
                TextureSize = regions[index].Size,
            };

            capePartDatas[index] = cropedCapeData;
            index++;
        }

        return capePartDatas;
    }

    public CapePartData Cut(CapeData capeData, CapePart capePart)
    {
        var capeBoundaries = TextureComponentsDictionary.CapeMappings[capePart];
        return new CapePartData(_imageUtilities)
        {
            CapePart = capePart,
            TextureBytes = _imageUtilities.CropImage((ReadOnlySpan<byte>)capeData.TextureBytes.AsSpan(), capeBoundaries),
            TextureSize = capeBoundaries.Size
        };
    }
}
